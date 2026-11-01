using KBank_Web_API.Models;

namespace KBank_Web_API.Repositories
{
    public interface ICompraParceladaRepository
    {
        Task<CompraParcelada> GetCompraAsync(int id, ApplicationUser user);
        Task<IEnumerable<CompraParcelada>> GetComprasAsync(ApplicationUser user);
        Task<CompraParcelada> CreatedAsync( CompraParcelada compraParcelada );
        Task<CompraParcelada> DeletedAsync(CompraParcelada compraParcelada);
        Task<CompraParcelada> SaveChangesAsync (CompraParcelada compraParcelada );
    }
}
