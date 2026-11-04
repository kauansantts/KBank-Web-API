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



        public async Task<Parcela> GetParcela(int id, ApplicationUser user)
        {
            var parcela = await _context.Parcelas.Where(c => c.compraParcelada.UsuarioId == user.Id).FirstOrDefaultAsync(p => p.ParcelaId == id) ?? throw new InvalidOperationException($"Este Id de parcela nao pertence ao usuario {user.UserName}!");
            return parcela;
            //var listComprasUser = user.ComprasParceladas.ToList();
            //foreach(var compra in listComprasUser)
            //{
            //    if(compra.CompraParceladaId == parcela.CompraParceladaId)
            //    {
            //        return parcela;
            //    }
            //}
            //throw new InvalidOperationException($"Este Id de parcela nao pertence ao usuario {user.UserName}!");
        }

        public async Task<IEnumerable<Parcela>> GetParcelas(ApplicationUser user)
        {
            //varias formas que achei de fazer, deixei a melhor opção rodando!
            //var parcelas = await _context.Parcelas.ToListAsync();
            var parcelas = await _context.Parcelas.Where(c => c.compraParcelada.UsuarioId == user.Id).ToListAsync();
            //var compras = await _context.ComprasParceladas.Include(p => p.Parcelas).Where(c => c.UsuarioId == user.Id).ToListAsync();
            //var parcelas = compras.SelectMany(p => p.Parcelas);

            return parcelas;
            ////List<Parcela> parcelasRetornadas = new List<Parcela>();
            ////foreach (var compra in compras)
            ////{
            ////    foreach (var parcela in parcelas)
            ////    {
            ////        if (parcela.CompraParceladaId == compra.CompraParceladaId)
            ////        {
            ////            parcelasRetornadas.Add(parcela);
            ////        }
            ////    }
            ////}

            ////if (!parcelasRetornadas.Any()) throw new ArgumentNullException("Este usuario nao possui parcelas!");
            ////return parcelasRetornadas;

        }
        public async Task<IEnumerable<Parcela>> GetParcelasVencidas(ApplicationUser user)
        {
            var dataAtual = DateTime.UtcNow;
            var parcelas = await _context.Parcelas.Where(p => p.compraParcelada.UsuarioId == user.Id).Where(p => p.DataParcela < dataAtual && p.Processada == false).ToListAsync();
            return parcelas;
        }

        public async Task<Parcela> DeletedParcela(Parcela parcela)
        {
            if (parcela is null)
            {
                throw new ArgumentNullException();
            }

            _context.Parcelas.Remove(parcela);
            await _context.SaveChangesAsync();

            return parcela;
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
