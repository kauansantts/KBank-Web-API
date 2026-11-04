using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;

namespace KBank_Web_API.Repositories;

public interface ITransacaoRepository
{
    public Task<IEnumerable<Transacao>> GetTransacoesAsync(ApplicationUser user);
    public Task<Transacao> GetTransacaoAsync(int id, ApplicationUser user);
    public Task<Transacao> GetTransacaoParcelaIdAsync(ApplicationUser user, int id);
    public Task<IEnumerable<Transacao>> GetTipoEnum(TipoEnum tipo, ApplicationUser user);
    public Task<IEnumerable<Transacao>> GetCategoriaEnum(CategoriaEnum categoria, ApplicationUser user);
    public Task<IEnumerable<Transacao>> GetIdentificadorEnum(IdentificadorEnum identificador, ApplicationUser user);
    public Task<Transacao> CreatedAsync(Transacao transacao);
    public Task SaveTransacaoAsync(Transacao transacao);
    public Task UpdateAsync(Transacao transacao);
}
