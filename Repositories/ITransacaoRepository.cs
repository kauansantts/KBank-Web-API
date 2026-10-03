using KBank_Web_API.Models;

namespace KBank_Web_API.Repositories;

public interface ITransacaoRepository
{
    public Task<IEnumerable<Transacao>> GetTransacoes();
    public Task<Transacao> GetTransacao(int id);
}
