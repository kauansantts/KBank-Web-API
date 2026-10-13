using KBank_Web_API.Models;

namespace KBank_Web_API.Service
{
    public interface ITransacaoService
    {
        public Task<Transacao> CancelarTransacao(int id);
    }
}
