using KBank_Web_API.Infra;
using KBank_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KBank_Web_API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Double> GetSaldo(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == id);

            //em manutenção ainda 06/10/26
            return usuario.SaldoAtual;
        }
    }
}
