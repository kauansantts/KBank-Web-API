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

        public async Task<CompraParcelada> GetCompraAsync(int id, ApplicationUser user)
        {
            var compra =  await _context.ComprasParceladas.Include(p => p.Parcelas).FirstOrDefaultAsync(c => c.CompraParceladaId == id) ?? throw new ArgumentNullException();
            var listCompraUser = user.ComprasParceladas.ToList();

            foreach(var c in listCompraUser)
            {
                if(c == compra)
                {
                    return compra;
                }
            }
            throw new InvalidOperationException($"Este ID de compra nao pertence ao usuario{user.UserName}");
        }

        public async Task<IEnumerable<CompraParcelada>> GetComprasAsync(ApplicationUser user)
        {
            var compras = await _context.ComprasParceladas.Include(p => p.Parcelas).Where(c => c.UsuarioId == user.Id).ToListAsync() ?? throw new ArgumentNullException("Usuario nao possui compras parceladas!");   
            return compras; 
        }

        public async Task<CompraParcelada> CreatedAsync(CompraParcelada compraParcelada)
        {
            if(compraParcelada is null)
            {
                throw new ArgumentNullException("Parametros invalidos, revise!");
            }

            compraParcelada.GerarParcelas();
            _context.ComprasParceladas.Add(compraParcelada);
            await _context.SaveChangesAsync();
            return compraParcelada;
        }

        public async Task<CompraParcelada> DeletedAsync(CompraParcelada compra)
        { 
            if (compra is null)
            {
                throw new ArgumentNullException();
            }

            _context.ComprasParceladas.Remove(compra);
            await _context.SaveChangesAsync();

            return compra;
        }

        public async Task<CompraParcelada> SaveChangesAsync(CompraParcelada compraParcelada)
        {
            await _context.SaveChangesAsync();
            return compraParcelada;
        }
    }
}
