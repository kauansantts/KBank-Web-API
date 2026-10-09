using KBank_Web_API.Models;

namespace KBank_Web_API.Service
{
    public interface IParcelaService
    {
        Task<IEnumerable<Parcela>> ProcessarParcelas();
    }
}
