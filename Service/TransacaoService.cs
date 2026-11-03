using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using KBank_Web_API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace KBank_Web_API.Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransacaoService(ITransacaoRepository transacaoRepo, UserManager<ApplicationUser> userManager)
        {
            _transacaoRepo = transacaoRepo;
            _userManager = userManager;
        }


        public async Task<Transacao> CancelarTransacao(int id, ApplicationUser user)
        {
            try
            {
                var transacao = await _transacaoRepo.GetTransacaoAsync(id, user);
                if(transacao.Identificador == IdentificadorEnum.CompraParcelada)
                {
                    throw new OperationCanceledException("Não é possivel cancelar uma transação parcelada, apenas cancelar a parcela!\nVá no endpoint de compra parcelada, e cancele a compra!");
                }

                var novoSaldo = transacao.ValorTransacao;
                if(transacao.TipoTransacao == TipoEnum.Despesa)
                {
                    user.SaldoUser += novoSaldo;
                }else if(transacao.TipoTransacao == TipoEnum.Receita)
                {
                    user.SaldoUser -= novoSaldo;
                }

                transacao.Cancelada = true;
                await _transacaoRepo.UpdateAsync(transacao);
                return transacao;
            }catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<IEnumerable<Transacao>> GetTransacaoData(DateTime dataInicial, DateTime dataFinal, ApplicationUser user)
        {
            var ListTransacoes = new List<Transacao>();
            var transacoes = await _transacaoRepo.GetTransacoesAsync(user);
            foreach (var transacao in transacoes)
            {
                if(transacao.DataTransacao >= dataInicial && transacao.DataTransacao <= dataFinal)
                {
                    ListTransacoes.Add(transacao);
                }
            }
            if (!ListTransacoes.Any())
            {
                throw new ArgumentNullException("Não possui transações nesse periodo!");
            }

            return ListTransacoes;
        }

        public async Task<bool> AtualizarSaldoUsuario(double saldoTransacao, TipoEnum tipo, ApplicationUser user)
        {
            if(tipo == TipoEnum.Despesa)
            {
                user.SaldoUser -= saldoTransacao;
            }
            else
            {
                user.SaldoUser += saldoTransacao;
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
