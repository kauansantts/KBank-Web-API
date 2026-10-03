using KBank_Web_API.Infra;
using KBank_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KBank_Web_API.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transacao> GetTransacao(int id)
    {
        return await _context.Transacoes.FirstOrDefaultAsync(t => t.TransacaoId == id);
    }

    public async Task<IEnumerable<Transacao>> GetTransacoes()
    {
        return await _context.Transacoes.ToListAsync();
    }
}
