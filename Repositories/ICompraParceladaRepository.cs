using KBank_Web_API.Models;

namespace KBank_Web_API.Repositories
{
    public interface ICompraParceladaRepository
    {
        CompraParcelada Created( CompraParcelada compraParcelada );
        Task<CompraParcelada> Deleted(int id);
    }
}
