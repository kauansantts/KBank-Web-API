using AutoMapper;
using KBank_Web_API.DTOs.Request;
using KBank_Web_API.DTOs.Response;
using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
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
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");
            
            try
            {
                var transacao = await _repository.GetTransacaoAsync(id, usuario);
                var transacaoDto = _mapper.Map<TransacaoResponseDTO>(transacao);
                return Ok(transacaoDto);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Transações")]
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetTransacoes()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");
            

            var transacoes = await _repository.GetTransacoesAsync(usuario);
            var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);

            return Ok(transacoesDto);
        }


        [HttpGet("/Por-data")]//passagem de parametros = 2026-01-20
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetPorData([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                var transacoes = await _transacaoService.GetTransacaoData(dataInicial, dataFinal, usuario);
                var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);
                return Ok(transacoesDto);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Por-Maior-Menor")]
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetPorOrdem([FromQuery] double valorInicial, [FromQuery] double valorFinal)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                var transacoes = await _transacaoService.GetPorOrdem(valorInicial, valorFinal, usuario);
                var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);
                return Ok(transacoesDto);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Por-compraParceladaId/{id}")]//id da compra parcelada
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetPorCompraParcelada(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                var transacoes = await _transacaoService.GetTransacaoCompra(id, usuario);
                var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);
                return Ok(transacoesDto);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Por-ParcelaId/{id}")]//id da parcela
        public async Task<ActionResult<TransacaoResponseDTO>> GetPorParcelaId(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                var transacoes = await _transacaoService.GetTransacaoParcela(id, usuario);
                var transacoesDto = _mapper.Map<TransacaoResponseDTO>(transacoes);
                return Ok(transacoesDto);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Por-Tipo")]
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetPorTipoEnum([FromQuery] TipoEnum tipo)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                var transacoes = await _transacaoService.GetTipoEnum(tipo, usuario);
                var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);
                return Ok(transacoesDto);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Por-Categoria")]
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetPorCategoriaEnum([FromQuery] CategoriaEnum categoria)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                var transacoes = await _transacaoService.GetCategoriaEnum(categoria, usuario);
                var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);
                return Ok(transacoesDto);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Por-Identificador")]
        public async Task<ActionResult<IEnumerable<TransacaoResponseDTO>>> GetPorIdentificadorEnum([FromQuery] IdentificadorEnum identificador)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            try
            {
                var transacoes = await _transacaoService.GetIdentificadorEnum(identificador, usuario);
                var transacoesDto = _mapper.Map<IEnumerable<TransacaoResponseDTO>>(transacoes);
                return Ok(transacoesDto);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TransacaoResponseDTO>> Created([FromBody]TransacaoRequestDTO transacaoRequestDTO)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?. Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");

            var transacao = _mapper.Map<Transacao>(transacaoRequestDTO);
            transacao.UsuarioId = usuario.Id;
            var saldoTransacao = transacao.ValorTransacao;
            var tipo = transacao.TipoTransacao;

            try
            {
                var atualizarSaldoUser = await _transacaoService.AtualizarSaldoUsuario(saldoTransacao, tipo, usuario);
                if (atualizarSaldoUser is false) return BadRequest("Falha ao salvar saldo do usuario no banco!\nVerifique com o ADM");

                var transacaoCriada = await _repository.CreatedAsync(transacao);

                var transacaoCriadaResponseDto = _mapper.Map<TransacaoResponseDTO>(transacaoCriada);
                return StatusCode(201, transacaoCriadaResponseDto);     
            }
            catch
            {
                return BadRequest("Algo inesperado aconteceu, verifique com o ADM");
            }    
        }

        //ver melhor como fica essa questao de "estorno" pois parcelas que ja venceram nao tem como cancelar, porque transacao que ja foi feita tem como?
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransacaoResponseDTO>> CancelarTransacao(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = await _userManager.FindByIdAsync(usuarioId!);
            if (usuario is null) return NotFound("Usuario não encontrado!");
            
            try
            {
                var transacaoDeletada = await _transacaoService.CancelarTransacao(id, usuario);
                var transacaoDeletadaDto = _mapper.Map<TransacaoResponseDTO>(transacaoDeletada);
                return Ok(transacaoDeletadaDto);
            }
            catch(OperationCanceledException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
