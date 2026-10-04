using KBank_Web_API.Models;

namespace KBank_Web_API.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<Double> GetSaldo(int id);
    }
}
