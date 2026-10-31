using AutoMapper;
using KBank_Web_API.DTOs.Response;
using KBank_Web_API.Models;
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
    public class ParcelaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParcelaRepository _repository;
        private readonly IParcelaService _parcelaService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ParcelaController(IMapper mapper, IParcelaRepository repository, IParcelaService parcelaService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _parcelaService = parcelaService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParcelaResponseDTO>> GetParcela(int id)
        {
            try
            {
                var parcela = await _repository.GetParcela(id);
                var parcelaDto = _mapper.Map<ParcelaResponseDTO>(parcela);
                return Ok(parcelaDto);
            }
            catch
            {
                return NotFound("ID invalido!");
            }           
        }

        [HttpGet("/Parcelas")]
        public async Task<ActionResult<IEnumerable<ParcelaResponseDTO>>> GetParcelas()
        {
            var parcelas = await _repository.GetParcelas();
            var parcelasDto = _mapper.Map<IEnumerable<ParcelaResponseDTO>>(parcelas);
            return Ok(parcelasDto);
        }



        [HttpPost("/Processar-parcelas")]
        public async Task<ActionResult<IEnumerable<ParcelaCompraResponseDTO>>> GerarTransacoesParceladas()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return BadRequest("Usuario não encontrado!");

            try
            {
                var processamento = await _parcelaService.ProcessarParcelas(usuario);
                var parcelasProcessadas = processamento.parcelas;
                if (!parcelasProcessadas.Any())
                {
                    return BadRequest("Você não tem parcelas para processar!");
                }
                var processamentoDto = _mapper.Map<IEnumerable<ParcelaCompraResponseDTO>>(parcelasProcessadas);
                return Ok(processamentoDto);
            }
            catch
            {
                return BadRequest("Algo inesperado aconteceu, verifique com o ADM");
            }
        }


        [HttpPost("Processar-parcela/{id}")]
        public async Task<ActionResult<ParcelaResponseDTO>> GerarTransacaoParcId(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return BadRequest("Usuario não encontrado!");
            
            try
            {
                var process = await _parcelaService.ProcessarParcelasId(id, usuario);
                var processDto = _mapper.Map<ParcelaResponseDTO>(process);
                return Ok(processDto);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return NotFound("ID invalido!");
            }
        }
    }
}
