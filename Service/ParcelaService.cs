using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using KBank_Web_API.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KBank_Web_API.Service
{
    public class ParcelaService : IParcelaService
    {
        private readonly IParcelaRepository _parcelaRepo;
        private readonly ITransacaoRepository _transacaoRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ParcelaService(IParcelaRepository parcelaRepo, ICompraParceladaRepository compraParceladaRepo, ITransacaoRepository transacaoRepo, UserManager<ApplicationUser> userManager)
        {
            _parcelaRepo = parcelaRepo;
            _transacaoRepo = transacaoRepo;
            _userManager = userManager;
        }

        public async Task<(IEnumerable<Transacao> transacoes, IEnumerable<Parcela> parcelas)> ProcessarParcelas(ApplicationUser user)
        {
            var dataAtual = DateTime.Now;
            var parcelas = await _parcelaRepo.GetParcelas();
            List<Parcela> ListParcelasProcessadas = new List<Parcela>();
            List<Transacao> ListTransacaoGeradas = new List<Transacao>();

            foreach ( var parcela in parcelas)
            {
                if(parcela.DataParcela <= dataAtual && parcela.Processada is false)
                {
                    var transacao = new Transacao()
                    {
                        DataTransacao = parcela.DataParcela,
                        Identificador = IdentificadorEnum.CompraParcelada,
                        CategoriaTransacao = parcela.CategoriaParcela,
                        Descricao = parcela.Descricao,
                        ValorTransacao = parcela.ValorParcela,
                        ParcelaId = parcela.ParcelaId,
                        TipoTransacao = parcela.TipoParcela,
                        UsuarioId = user.Id
                    };
                    var SaveSaldoUser = await AtualizarSaldoUsuario(transacao.ValorTransacao, transacao.TipoTransacao, user);
                    if (SaveSaldoUser is false)
                    {
                        throw new InvalidOperationException("Falha ao salvar alterações no banco!");
                    }
                    ListTransacaoGeradas.Add(transacao);
                    await _transacaoRepo.SaveTransacaoAsync(transacao);
                    parcela.Processada = true;
                    await _parcelaRepo.SaveChangesAsync(parcela);
                    ListParcelasProcessadas.Add(parcela);
                }
            }

            return (ListTransacaoGeradas, ListParcelasProcessadas);
        }

        public async Task<Parcela> ProcessarParcelasId(int id, ApplicationUser user)
        {
            
            var parcela = await _parcelaRepo.GetParcela(id);
            if(parcela is null)
            {
                throw new ArgumentNullException();
            }

            var transacao = new Transacao()
            {
                DataTransacao = parcela.DataParcela,
                Identificador = IdentificadorEnum.CompraParcelada,
                CategoriaTransacao = parcela.CategoriaParcela,
                Descricao = parcela.Descricao,
                ValorTransacao = parcela.ValorParcela,
                ParcelaId = parcela.ParcelaId,
                TipoTransacao = parcela.TipoParcela,
                UsuarioId = user.Id
            };
            var SaveSaldoUser = await AtualizarSaldoUsuario(transacao.ValorTransacao, transacao.TipoTransacao, user);
            if(SaveSaldoUser is false)
            {
                throw new InvalidOperationException("Falha ao salvar alterações no banco!");
            }
            await _transacaoRepo.SaveTransacaoAsync(transacao);
            parcela.Processada = true;
            await _parcelaRepo.SaveChangesAsync(parcela);

            return parcela;
        }

        public async Task<bool> AtualizarSaldoUsuario(double saldoParcela, TipoEnum tipo, ApplicationUser user)
        {
            if (tipo == TipoEnum.Despesa)
            {
                user.SaldoUser -= saldoParcela;
            }
            else
            {
                user.SaldoUser += saldoParcela;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;

        }
    }
}
