using KBank_Web_API.DTOs.AuthDtos;
using KBank_Web_API.DTOs.Response;
using KBank_Web_API.Models;
using KBank_Web_API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KBank_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IConfiguration config, ITokenService tokenService, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        
        [HttpPost("/Login")]
        public async Task<IActionResult> login([FromBody] LoginDTO request)
        {
            var user = await _userManager.FindByNameAsync(request.NomeUsuario);
            if(user is not null && await _userManager.CheckPasswordAsync(user, request.Senha))
            {
                var rolesUser = await _userManager.GetRolesAsync(user);

                var ClaimUser = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!)
                };

                foreach(var role in rolesUser)
                {
                    ClaimUser.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = _tokenService.GerarAccessToken(ClaimUser, _config);
                var refreshToken = _tokenService.GerarRefreshToken();
                var lifeTimeRefresh = _config.GetSection("JWT").GetValue<int>("RefreshTokenValidityInMinutes");

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpireTime = DateTime.UtcNow.AddMinutes(lifeTimeRefresh);
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest("Erro ao salvar alterações no banco\n" + result.Errors);
                }

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expired = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost("/Cadastro")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var userExistente = await _userManager.FindByNameAsync(request.NomeUsuario);
            if(userExistente != null)
            {
                return BadRequest("Usuario ja cadastrado!");
            }

            var user = new ApplicationUser()
            {
                UserName = request.NomeUsuario,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                SaldoUser = request.SaldoUser
            };

            var result = await _userManager.CreateAsync(user, request.Senha);
            if (!result.Succeeded) return BadRequest("Erro ao salvar alterações no banco\n" + result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Usuario");
            if (!roleResult.Succeeded) return BadRequest("falha ao adicionar role ao usuario!");

            return StatusCode(201, new
            {
                message = "Usuario criado com sucesso!",
                Id = user.Id,
                Nome = user.UserName,
                Email = user.Email,
                Saldo = user.SaldoUser.ToString("F2")
            });
        }

        [HttpPost("/Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDTO request)
        {
            if (request is null) return BadRequest("Tokens invalidos!");
            
            var tokenRequest = request.AccessToken;
            var refreshTokenRequest = request.RefreshToken;

            var claimsPrincipal = _tokenService.GetPrincipalClaims(tokenRequest, _config);
            if(claimsPrincipal is null) return NotFound("Claims vazias!");
            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId!);

            if(user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpireTime <= DateTime.UtcNow)
            {
                return BadRequest("Operação invalida! Possiveis causas:\n-Usuario invalido\n-RefreshToken invalido\n-Validade do token expirada\nVerifique melhor!");
            }

            var authClaims = await _tokenService.AtualizarClaimsUsuario(user);
            if (authClaims is null) return BadRequest("Claims vazias!");
            var novoToken = _tokenService.GerarAccessToken(authClaims, _config);
            var novoRefresh = _tokenService.GerarRefreshToken();
            var novoLifeTime = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT").GetValue<int>("RefreshTokenValidityInMinutes"));

            user.RefreshToken = novoRefresh;
            user.RefreshTokenExpireTime = novoLifeTime;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Erro ao salvar alterações no banco\n" + result.Errors);
            }

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(novoToken),
                ExpiredToken = novoToken.ValidTo,
                RefreshToken = novoRefresh,
                ExpiredRefreshToken = novoLifeTime,
            });
        }

        [HttpGet("/Dados-Usuário")]
        public async Task<IActionResult> GetDadosUser()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return BadRequest("Usuario não encontrado!");

            return Ok(new
            {
                Id = usuario.Id,
                Nome = usuario.UserName,
                Email = usuario.Email,
                Saldo = usuario.SaldoUser.ToString("F2")
            });
        }

        [Authorize(Policy = "SuperAdmOnly")]
        [HttpGet("/Todos-Usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            return Ok(usuarios);
        }

        [Authorize(Policy = "SuperAdmOnly")]
        [HttpPost("/Create-Role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist) return BadRequest($"Role {roleName} ja existente!");

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded) return BadRequest($"Falha na criação da role {roleName}\n-{roleResult.Errors}");

            return Ok($"Role {roleName} criada com sucesso!");
        }

        [Authorize(Policy = "SuperAdmOnly")]
        [HttpPost("/AddToRole-User")]
        public async Task<IActionResult> AddRoleUser(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName!);
            if(user is null) return NotFound($"Usuario de nome {userName} não encontrado!");

            var roles = await _roleManager.Roles.ToListAsync();
            var queryRoles = roles.Any(r => r.ToString() == roleName);
            if (!queryRoles) return NotFound($"Role de nome {roleName} não encontrado!");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded) return BadRequest($"Falha ao adicionar a role {roleName} ao usuario {user.UserName}\n-{result.Errors}");

            return Ok($"Role {roleName} adicionado ao usuario {user.UserName} com sucesso!");
        }
    }
}
