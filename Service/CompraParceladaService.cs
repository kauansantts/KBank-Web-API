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

        public async Task<CompraParcelada> CancelarCompraParcelada(int id, ApplicationUser user)
        {
            try
            {
                var compra = await _compraRepo.GetCompraAsync(id, user);

                //var parcelas = await _parcelaRepo.GetParcelas();
                //foreach (var parcela in parcelas)
                //{
                //    if (parcela.CompraParceladaId == compra.CompraParceladaId && parcela.Processada == false)
                //    {
                //        await _parcelaRepo.DeletedParcela(parcela);
                //        compra.Cancelada = true;
                //    }
                //}
                foreach (var parcela in compra.Parcelas)
                {
                    if (parcela.CompraParceladaId == compra.CompraParceladaId && parcela.Processada == false)
                    {
                        await _parcelaRepo.DeletedParcela(parcela);
                        compra.Cancelada = true;
                    }
                }
                await _compraRepo.SaveChangesAsync(compra);
                return compra;
            } catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch
            {
                throw new Exception();
            }

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
