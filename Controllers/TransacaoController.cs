using AutoMapper;
using KBank_Web_API.DTOs.Request;
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
    public class TransacaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransacaoRepository _repository;
        private readonly ITransacaoService _transacaoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransacaoController(IMapper mapper, ITransacaoRepository repository, ITransacaoService transacaoService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _transacaoService = transacaoService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransacaoResponseDTO>> GetTransacao(int id)
        {
            try
            {
                var transacao = await _repository.GetTransacaoAsync(id);
                var transacaoDto = _mapper.Map<TransacaoResponseDTO>(transacao);
                return Ok(transacaoDto);
            }
            catch
            {
                return NotFound("ID invalido!");
            }
        }

        [HttpGet("/Transações")]
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetTransacoes()
        {
            var transacoes = await _repository.GetTransacoesAsync();
            var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);

            return Ok(transacoesDto);
        }


        [HttpGet("/Transações-por-data")]//passagem de parametros = 2026-01-20
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetTransacoesData([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
        {
            try
            {
                var transacoes = await _transacaoService.GetTransacaoData(dataInicial, dataFinal);
                var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);
                return Ok(transacoesDto);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]//por enquanto nao vai funcionar pois tem que fazer a relação com o usuario ID
        public async Task<ActionResult<TransacaoResponseDTO>> Created([FromBody]TransacaoRequestDTO transacaoRequestDTO)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?. Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return BadRequest("Usuario não encontrado!");

            var transacao = _mapper.Map<Transacao>(transacaoRequestDTO);
            transacao.UsuarioId = usuario.Id;

            try
            {
                var transacaoCriada = await _repository.CreatedAsync(transacao);
                var transacaoCriadaResponseDto = _mapper.Map<TransacaoResponseDTO>(transacaoCriada);
                return StatusCode(201, transacaoCriadaResponseDto);     
            }
            catch
            {
                return BadRequest("Algo inesperado aconteceu, verifique com o ADM");
            }    
        }

        [HttpDelete]
        public async Task<ActionResult<TransacaoResponseDTO>> CancelarTransacao(int id)
        {
            try
            {
                var transacaoDeletada = await _transacaoService.CancelarTransacao(id);
                var transacaoDeletadaDto = _mapper.Map<TransacaoResponseDTO>(transacaoDeletada);
                return Ok(transacaoDeletadaDto);
            }
            catch(ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }catch(OperationCanceledException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
