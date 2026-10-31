using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using KBank_Web_API.Repositories;
using System.Security.Claims;

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

        public async Task<(IEnumerable<Transacao> transacoes, IEnumerable<Parcela> parcelas)> ProcessarParcelas(ApplicationUser user)
        {
            var dataAtual = DateTime.Now;
            var parcelas = await _parcelaRepo.GetParcelas();
            List<Parcela> ListParcelasProcessadas = new List<Parcela>();
            List<Transacao> ListTransacaoGeradas = new List<Transacao>();

            foreach ( var parcela in parcelas)
            {
                if(parcela.DataParcela <= dataAtual && parcela.Processada is false)
                {
                    var transacao = new Transacao()
                    {
                        DataTransacao = parcela.DataParcela,
                        Identificador = IdentificadorEnum.CompraParcelada,
                        CategoriaTransacao = parcela.CategoriaParcela,
                        Descricao = parcela.Descricao,
                        ValorTransacao = parcela.ValorParcela,
                        ParcelaId = parcela.ParcelaId,
                        TipoTransacao = parcela.TipoParcela,
                        UsuarioId = user.Id
                    };
                    ListTransacaoGeradas.Add(transacao);
                    await _transacaoRepo.SaveTransacaoAsync(transacao);
                    parcela.Processada = true;
                    await _parcelaRepo.SaveChangesAsync(parcela);
                    ListParcelasProcessadas.Add(parcela);
                }
            }

            return (ListTransacaoGeradas, ListParcelasProcessadas);
        }

        public async Task<Parcela> ProcessarParcelasId(int id, ApplicationUser user)
        {
            
            var parcela = await _parcelaRepo.GetParcela(id);
            if(parcela is null)
            {
                throw new ArgumentNullException();
            }

            var transacao = new Transacao()
            {
                DataTransacao = parcela.DataParcela,
                Identificador = IdentificadorEnum.CompraParcelada,
                CategoriaTransacao = parcela.CategoriaParcela,
                Descricao = parcela.Descricao,
                ValorTransacao = parcela.ValorParcela,
                ParcelaId = parcela.ParcelaId,
                TipoTransacao = parcela.TipoParcela,
                UsuarioId = user.Id
            };
            await _transacaoRepo.SaveTransacaoAsync(transacao);
            parcela.Processada = true;
            await _parcelaRepo.SaveChangesAsync(parcela);

            return parcela;
        }
    }
}
