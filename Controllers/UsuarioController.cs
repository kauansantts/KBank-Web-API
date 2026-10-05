using KBank_Web_API.Models;
using KBank_Web_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBank_Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet("saldo")]
    public async Task<ActionResult<Double>> Get(int id)
    {
        var usersaldo = await _usuarioRepository.GetSaldo(id);
        if (usersaldo == 0)
        {
            return NotFound();
        }
        return usersaldo;
    } 
}
