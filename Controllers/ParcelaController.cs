using AutoMapper;
using KBank_Web_API.DTOs;
using KBank_Web_API.Repositories;
using KBank_Web_API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBank_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParcelaRepository _repository;
        private readonly IParcelaService _parcelaService;

        public ParcelaController(IMapper mapper, IParcelaRepository repository, IParcelaService parcelaService)
        {
            _mapper = mapper;
            _repository = repository;
            _parcelaService = parcelaService;
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
            try
            {
                var processamento = await _parcelaService.ProcessarParcelas();
            if (!processamento.Any())
            {
                return BadRequest("Você não tem parcelas para processar!");
            }
            var processamentoDto = _mapper.Map<IEnumerable<ParcelaCompraResponseDTO>>(processamento);

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
            try
            {
                var process = await _parcelaService.ProcessarParcelasId(id);
                var processDto = _mapper.Map<ParcelaResponseDTO>(process);
                return Ok(processDto);
            }
            catch
            {
                return NotFound("ID invalido!");
            }
        }
    }
}
