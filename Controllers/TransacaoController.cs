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

        public TransacaoController(IMapper mapper, ITransacaoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
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
            var transacoesDto = _mapper.Map<TransacaoResponseDTO>(transacoes);

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

        //[HttpDelete] CONTINUAR AQUI...RACIOCINIO!!
        //public async Task<ActionResult<TransacaoResponseDTO>> Deleted(int id)
        //{
        //    try
        //    {
        //        var transacaoDeletada = await _repository.CancelarCompraParcelada(id);
        //        var compraDeletadaDTO = _mapper.Map<CompraParceladaResponseDTO>(compraDeletada);
        //        return Ok(compraDeletadaDTO);
        //    }
        //    catch
        //    {
        //        return BadRequest("ID invalido/erro inesperado, verifique com o ADM!");
        //    }
        //}


        //[HttpDelete] SO DEMONSTRAÇÃO PARA FAZER O DE CIMA!
        //public async Task<ActionResult<CompraParceladaResponseDTO>> Deleted(int id)
        //{
        //    try
        //    {
        //        var compraDeletada = await _compraService.CancelarCompraParcelada(id);
        //        var compraDeletadaDTO = _mapper.Map<CompraParceladaResponseDTO>(compraDeletada);
        //        return Ok(compraDeletadaDTO);
        //    }
        //    catch
        //    {
        //        return BadRequest("ID invalido/erro inesperado, verifique com o ADM!");
        //    }
        //}
    }
}
