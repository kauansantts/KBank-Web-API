using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using KBank_Web_API.Repositories;

namespace KBank_Web_API.Service
{
    public class CompraParceladaService : ICompraParceladaService
    {    
        private readonly ICompraParceladaRepository _compraRepo;
        private readonly IParcelaRepository _parcelaRepo;

        public CompraParceladaService(ICompraParceladaRepository compraRepo, IParcelaRepository parcelaRepo)
        {
            _compraRepo = compraRepo;
            _parcelaRepo = parcelaRepo;
        }

        public async Task<CompraParcelada> CancelarCompraParcelada(int id)
        {
            var compra = await _compraRepo.GetCompraAsync(id);
            if (compra is null)
            {
                throw new ArgumentNullException();
            }

            var parcelas = await _parcelaRepo.GetParcelas();
            foreach(var parcela in parcelas)
            {
                if(parcela.CompraParceladaId == compra.CompraParceladaId && parcela.Processada == false)
                {
                    await _parcelaRepo.DeletedParcela(parcela);
                    compra.Cancelada = true;
                }
            }
            await _compraRepo.SaveChangesAsync(compra);
            return compra;
        }

        public CompraParcelada VerificarCategoria(CompraParcelada compra)
        {
            if (compra.CategoriaParcela == CategoriaEnum.fixa)
            {
                throw new ArgumentNullException();
            }
            return compra;
        }
    }
}
