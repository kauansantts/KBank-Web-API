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
    public class CompraParceladaController : ControllerBase
    {
        private readonly ICompraParceladaRepository _compra;
        private readonly IMapper _mapper;
        private readonly ICompraParceladaService _compraService;

        public CompraParceladaController(ICompraParceladaRepository compra, IMapper mapper, ICompraParceladaService compraService)
        {
            _compra = compra;
            _mapper = mapper;
            _compraService = compraService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompraParceladaResponseDTO>> GetCompra(int id)
        {
            var compra = await _compra.GetCompraAsync(id);
            if(compra is null)
            {
                return NotFound("ID invalido");
            }

            var CompraDTO = _mapper.Map<CompraParceladaResponseDTO>(compra);
            return Ok(CompraDTO);
        }

        [HttpGet("/Compras parceladas")]
        public async Task<IEnumerable<CompraParceladaResponseDTO>> GetCompras()
        {
            var compras = await _compra.GetComprasAsync();
            var comprasDTO = _mapper.Map<IEnumerable<CompraParceladaResponseDTO>>(compras);
            return comprasDTO;
        }
        
        [HttpPost]
        public async  Task<ActionResult<CompraParceladaResponseDTO>> Created([FromBody]CompraParceladaRequestDTO compraParceladaRequestDto)
        {

            var compraParcelada = _mapper.Map<CompraParcelada>(compraParceladaRequestDto);
            try
            {
                var compraP = await _compra.CreatedAsync(compraParcelada);
                var compraDTO = _mapper.Map<CompraParceladaResponseDTO>(compraP);
                return StatusCode(201, compraDTO);
            }catch
            {
                return BadRequest("Algo inesperado aconteceu, verifique com o ADM");
            }
        }
        [HttpPost("/Add Compra Parcelada data antiga teste")]
        public async Task<ActionResult<CompraParceladaResponseDTO>> Created2([FromBody] CompraParcelaProvisorioDTO compraParceladaDto)
        {
            var compraParcelada = _mapper.Map<CompraParcelada>(compraParceladaDto);

            try
            {
                var compraP = await _compra.CreatedAsync(compraParcelada);
                var compraDTO = _mapper.Map<CompraParceladaResponseDTO>(compraP);
                return StatusCode(201, compraDTO);
            }
            catch
            {
                return BadRequest("Algo inesperado aconteceu, verifique com o ADM");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<CompraParceladaResponseDTO>> Deleted(int id)
        {
            try
            {
                var compraDeletada = await _compraService.DeletarCompraParcelada(id);
                var compraDeletadaDTO = _mapper.Map<CompraParceladaResponseDTO>(compraDeletada);
                return Ok(compraDeletadaDTO);
            }catch
            {
                return BadRequest("ID invalido/erro inesperado, verifique com o ADM!");
            }
        }
    }
}
