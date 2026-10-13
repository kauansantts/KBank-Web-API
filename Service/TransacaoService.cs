using KBank_Web_API.Models;
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
                throw new ArgumentNullException();
            }

            transacao.Cancelada = true;
            await _transacaoRepo.SaveChangesAsync(transacao);
            return transacao;
        }
    }
}
