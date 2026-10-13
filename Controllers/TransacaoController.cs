using AutoMapper;
using KBank_Web_API.DTOs;
using KBank_Web_API.Models;
using KBank_Web_API.Repositories;
using KBank_Web_API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBank_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransacaoRepository _repository;
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(IMapper mapper, ITransacaoRepository repository, ITransacaoService transacaoService)
        {
            _mapper = mapper;
            _repository = repository;
            _transacaoService = transacaoService;
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

        [HttpPost]
        public async Task<ActionResult<TransacaoResponseDTO>> Created([FromBody]TransacaoRequestDTO transacaoRequestDTO)
        {
            var transacao = _mapper.Map<Transacao>(transacaoRequestDTO);
            
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
