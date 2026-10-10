using KBank_Web_API.Models;
using KBank_Web_API.Repositories;

namespace KBank_Web_API.Service
{
    public class CompraParceladaService : ICompraParceladaService
    {    
        private readonly ICompraParceladaRepository _compraRepo;
        private readonly IParcelaRepository _parcelaRepo;
        private readonly ITransacaoRepository _transacaoRepo;

        public CompraParceladaService(ICompraParceladaRepository compraRepo, IParcelaRepository parcelaRepo, ITransacaoRepository transacaoRepo)
        {
            _compraRepo = compraRepo;
            _parcelaRepo = parcelaRepo;
            _transacaoRepo = transacaoRepo;
        }

        public async Task<CompraParcelada> DeletarCompraParcelada(int id)
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
                    }
                }

            await _compraRepo.DeletedAsync(compra);
            return compra;
        }
    }
}
