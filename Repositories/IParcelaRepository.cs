using KBank_Web_API.Models;

namespace KBank_Web_API.Repositories
{
    public interface IParcelaRepository
    {
        public Task<IEnumerable<Parcela>> GetParcelas(ApplicationUser user);
        public Task<Parcela> GetParcela(int id, ApplicationUser user);
        public Task SaveChangesAsync(Parcela parcela);
        public Task<Parcela> DeletedParcela(Parcela parcela);
    }
}
