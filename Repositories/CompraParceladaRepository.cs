using KBank_Web_API.Infra;
using KBank_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KBank_Web_API.Repositories
{
    public class CompraParceladaRepository : ICompraParceladaRepository
    {
        private readonly AppDbContext _context;

        public CompraParceladaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CompraParcelada> GetCompraAsync(int id)
        {
            return await _context.ComprasParceladas.FindAsync(id);
        }

        public async Task<IEnumerable<CompraParcelada>> GetComprasAsync()
        {
            var compras = await _context.ComprasParceladas.ToListAsync();
            return compras; 
        }

        public async Task<CompraParcelada> CreatedAsync(CompraParcelada compraParcelada)
        {
            if(compraParcelada is null)
            {
                throw new ArgumentNullException(nameof(compraParcelada));
            }

            _context.ComprasParceladas.Add(compraParcelada);
            await _context.SaveChangesAsync();
            return compraParcelada;
        }

        public async Task<CompraParcelada> DeletedAsync(int id)
        {
            var compra = await _context.ComprasParceladas.FirstOrDefaultAsync(c => c.CompraParceladaId == id);
            if (compra is null)
            {
                throw new ArgumentNullException();

            }

            _context.ComprasParceladas.Remove(compra);
            await _context.SaveChangesAsync();

            return compra;
        }

    }
}
