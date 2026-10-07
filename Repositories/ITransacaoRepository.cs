using KBank_Web_API.Models;

namespace KBank_Web_API.Repositories;

public interface ITransacaoRepository
{
    public Task<IEnumerable<Transacao>> GetTransacoesAsync();
    public Task<Transacao> GetTransacaoAsync(int id);
    public Task<Transacao> CreatedAsync(Transacao transacao);
}
