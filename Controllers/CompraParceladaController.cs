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
    public class CompraParceladaController : ControllerBase
    {
        private readonly ICompraParceladaRepository _compra;
        private readonly IMapper _mapper;

        public CompraParceladaController(ICompraParceladaRepository compra, IMapper mapper)
        {
            _compra = compra;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompraParceladaResponseDTO>> GetCompra(int id)
        {
            var compra = await _compra.GetCompraAsync(id);
            if(compra is null)
            {
                return NotFound("ID não encontrado");
            }

            var CompraDTO = _mapper.Map<CompraParceladaResponseDTO>(compra);
            return Ok(CompraDTO);
        }

        [HttpGet("/Compras parceladas")]
        public async Task<IEnumerable<CompraParcelada>> GetCompras()
        {
            return await _compra.GetComprasAsync();
        }
        
        [HttpPost]
        public async  Task<ActionResult<CompraParcelada>> Created([FromBody]CompraParcelada compraParcelada)
        {
            try
            {
                var compraP = await _compra.CreatedAsync(compraParcelada);
                return StatusCode(201, compraP);
            }catch (Exception ex)
            {
                return BadRequest("Dados invalidos");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CompraParcelada>> Deleted(int id)
        {
            try
            {
                return Ok(await _compra.DeletedAsync(id));
            }catch (Exception ex)
            {
                return BadRequest("ID invalido");
            }
        }
    }
}
