using AutoMapper;
using KBank_Web_API.DTOs;
using KBank_Web_API.Models;
using KBank_Web_API.Repositories;
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

        public TransacaoController(IMapper mapper, ITransacaoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetTransacoes()
        {
            var transacoes = await _repository.GetTransacoesAsync();

            return Ok(transacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transacao>> GetTransacao(int id)
        {
            try
            {
                var transacao = await _repository.GetTransacaoAsync(id);
                return Ok(transacao);
            }
            catch
            {
                return NotFound("ID invalido!");
            }
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

        //[HttpPost("/Processar parcelas")]
        //public async Task<ActionResult<TransacaoResponseDTO>> GerarTransacoesParceladas()
        //{
        //    try
        //    {
        //        //continuar metodo para chamar o parcelaService!
        //    }
        //}
    }
}
