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


        public async Task<Transacao> CancelarTransacao(int id)
        {
            var transacao = await _transacaoRepo.GetTransacaoAsync(id);
            if(transacao is null)
            {
                throw new ArgumentNullException("ID invalido");
            }

            if(transacao.Identificador == IdentificadorEnum.CompraParcelada)
            {
                throw new OperationCanceledException("Não é possivel cancelar uma transação parcelada\nApenas cancelar a parcela!");
            }

            transacao.Cancelada = true;
            await _transacaoRepo.UpdateAsync(transacao);
            return transacao;
        }

        public async Task<IEnumerable<Transacao>> GetTransacaoData(DateTime dataInicial, DateTime dataFinal)
        {
            var ListTransacoes = new List<Transacao>();
            var transacoes = await _transacaoRepo.GetTransacoesAsync();
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
