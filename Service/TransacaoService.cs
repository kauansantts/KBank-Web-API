using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using KBank_Web_API.Repositories;

namespace KBank_Web_API.Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepo;

        public TransacaoService(ITransacaoRepository transacaoRepo)
        {
            _transacaoRepo = transacaoRepo;
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
    }
}
