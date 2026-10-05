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

        public CompraParceladaController(ICompraParceladaRepository compra)
        {
            _compra = compra;
        }

        [HttpPost]
        public async Task<ActionResult<CompraParcelada>> Created([FromBody]CompraParcelada compraParcelada)
        {
            var compraP = _compra.Created(compraParcelada);
            if (compraP is null)
            {
                return BadRequest("Dados invalidos");
            }

            return StatusCode(201, "Criado com  sucesso!");
        }
    }
}
