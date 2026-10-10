using KBank_Web_API.Models;

namespace KBank_Web_API.Service
{
    public interface ICompraParceladaService
    {
        public Task<CompraParcelada> DeletarCompraParcelada(int id);
    }
}
