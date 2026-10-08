using KBank_Web_API.Infra;
using KBank_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KBank_Web_API.Repositories
{
    public class ParcelaRepository : IParcelaRepository
    {
        private readonly AppDbContext _context;

        public ParcelaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Parcela> GetParcela(int id)
        {
            var parcela = await _context.Parcelas.FirstOrDefaultAsync(p => p.ParcelaId == id);
            if (parcela is null)
            {
                throw new ArgumentNullException();
            }

            return parcela;
        }

        public async Task<IEnumerable<Parcela>> GetParcelas()
        {
            return await _context.Parcelas.ToListAsync();
        }

        public async Task SaveChangesAsync(Parcela parcela)
        {
            if(parcela is null)
            {
                throw new ArgumentNullException(nameof(parcela));
            }

            var parcelaSalva = _context.Update(parcela);
            await _context.SaveChangesAsync();
        }
    }
}
