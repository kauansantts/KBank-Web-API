using KBank_Web_API.Models;
using KBank_Web_API.Repositories;

namespace KBank_Web_API.Service
{
    public class ParcelaService : IParcelaService
    {
        private readonly IParcelaRepository _parcelaRepo;
        private readonly ITransacaoRepository _transacaoRepo;

        public ParcelaService(IParcelaRepository parcelaRepo, ICompraParceladaRepository compraParceladaRepo, ITransacaoRepository transacaoRepo)
        {
            _parcelaRepo = parcelaRepo;
            _transacaoRepo = transacaoRepo;
        }

        public async Task<IEnumerable<Parcela>> ProcessarParcelas()
        {
            var dataAtual = DateTime.Now;
            var parcelas = await _parcelaRepo.GetParcelas();
            List<Parcela> ListParcelasProcessadas = new List<Parcela>();

            foreach ( var parcela in parcelas)
            {
                if(parcela.DataParcela <= dataAtual && parcela.Processada is false)
                {
                    var transacao = new Transacao()
                    {
                        DataTransacao = parcela.DataParcela,
                        CategoriaTransacao = parcela.CategoriaParcela,
                        Descricao = null,
                        ValorTransacao = parcela.ValorParcela,
                        ParcelaId = parcela.ParcelaId,
                        TipoTransacao = parcela.TipoParcela
                    };
                    await _transacaoRepo.SaveChangesAsync(transacao);
                    parcela.Processada = true;
                    await _parcelaRepo.SaveChangesAsync(parcela);
                    ListParcelasProcessadas.Add(parcela);
                }
            }

            return ListParcelasProcessadas;
        }
    }
}
