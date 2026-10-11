using KBank_Web_API.Models;

namespace KBank_Web_API.Repositories
{
    public interface ICompraParceladaRepository
    {
        Task<CompraParcelada> GetCompraAsync(int id);
        Task<IEnumerable<CompraParcelada>> GetComprasAsync();
        Task<CompraParcelada> CreatedAsync( CompraParcelada compraParcelada );
        Task<CompraParcelada> DeletedAsync(CompraParcelada compraParcelada);
        Task<CompraParcelada> SaveChangesAsync (CompraParcelada compraParcelada );
    }
}
