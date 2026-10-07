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

    public async Task<Transacao> GetTransacaoAsync(int id)
    {
        var transacao = await _context.Transacoes.FirstOrDefaultAsync(t => t.TransacaoId == id);
        if(transacao is null)
        {
            throw new ArgumentNullException();
        }

        return transacao;
    }

    public async Task<IEnumerable<Transacao>> GetTransacoesAsync()
    {
        return await _context.Transacoes.ToListAsync();
    }

    public async Task<Transacao> CreatedAsync(Transacao transacao)
    {
        if (transacao is null)
        {
            throw new ArgumentNullException(nameof(transacao));
        }

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
        return transacao;
    }
}
