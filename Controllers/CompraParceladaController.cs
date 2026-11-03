using AutoMapper;
using KBank_Web_API.DTOs.Request;
using KBank_Web_API.DTOs.Response;
using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using KBank_Web_API.Repositories;
using KBank_Web_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KBank_Web_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompraParceladaController : ControllerBase
    {
        private readonly ICompraParceladaRepository _compra;
        private readonly IMapper _mapper;
        private readonly ICompraParceladaService _compraService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompraParceladaController(ICompraParceladaRepository compra, IMapper mapper, ICompraParceladaService compraService, UserManager<ApplicationUser> userManager)
        {
            _compra = compra;
            _mapper = mapper;
            _compraService = compraService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompraParceladaResponseDTO>> GetCompra(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");
            
            try
            {
                var compra = await _compra.GetCompraAsync(id, usuario);
                var CompraDTO = _mapper.Map<CompraParceladaResponseDTO>(compra);
                return Ok(CompraDTO);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return BadRequest("Operação invalida!");
            }
        }

        [HttpGet("/Compras-parceladas")]
        public async Task<ActionResult<IEnumerable<CompraParceladaResponseDTO>>> GetCompras()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            var compras = await _compra.GetComprasAsync(usuario);
            var comprasDTO = _mapper.Map<IEnumerable<CompraParceladaResponseDTO>>(compras);
            return Ok(comprasDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CompraParceladaResponseDTO>> Created([FromBody] CompraParceladaRequestDTO compraParceladaDto)
        {
            var compraParcelada = _mapper.Map<CompraParcelada>(compraParceladaDto);
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                _compraService.VerificarCategoria(compraParcelada);
            }
            catch
            {
                return BadRequest("Categoria da transacao é obrigatorio e so aceita livre e inesperada");
            }

            try
            {
                compraParcelada.UsuarioId = usuario.Id;
                var compraP = await _compra.CreatedAsync(compraParcelada);
                var compraDTO = _mapper.Map<CompraParceladaResponseDTO>(compraP);
                return StatusCode(201, compraDTO);
            }catch(ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return BadRequest("Algo inesperado aconteceu, verifique com o ADM");
            }
        }

        
        [HttpDelete]
        public async Task<ActionResult<CompraParceladaResponseDTO>> Deleted(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                var compraDeletada = await _compraService.CancelarCompraParcelada(id, usuario);
                var compraDeletadaDTO = _mapper.Map<CompraParceladaResponseDTO>(compraDeletada);
                return Ok(compraDeletadaDTO);
            }catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return BadRequest("erro inesperado, verifique com o ADM!");
            }
        }
    }
}
