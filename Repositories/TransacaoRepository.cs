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

    public async Task<Transacao> GetTransacaoAsync(int id, ApplicationUser user)
    {
        var transacao = await _context.Transacoes.Where(c => c.UsuarioId == user.Id).FirstOrDefaultAsync(t => t.TransacaoId == id) ?? throw new InvalidOperationException($"Este Id de transação nao pertence ao usuário {user.UserName}!");

        return transacao;
    }

    public async Task<IEnumerable<Transacao>> GetTransacoesAsync(ApplicationUser user)
    {
       return await _context.Transacoes.Where(c => c.UsuarioId == user.Id).ToListAsync();
    }

    public async Task<Transacao> CreatedAsync(Transacao transacao)
    {
        if (transacao is null)
        {
            throw new ArgumentNullException(nameof(transacao));
        }

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();//esta dando erro aqui dbcontextUpdate
        return transacao;
    }



    public async Task SaveTransacaoAsync(Transacao transacao)
    {
        if(transacao is null)
        {
            throw new ArgumentNullException(nameof(transacao));
        }

        _context.Add(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Transacao transacao)
    {
        if(transacao is null)
        {
            throw new ArgumentNullException();
        }

        _context.Update(transacao);
        await _context.SaveChangesAsync();
    }
}
