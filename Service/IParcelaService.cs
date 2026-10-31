using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using System.Security.Claims;

namespace KBank_Web_API.Service
{
    public interface IParcelaService
    {
        //Task<IEnumerable<Parcela>> ProcessarParcelas();
        Task<(IEnumerable<Transacao> transacoes, IEnumerable<Parcela> parcelas)> ProcessarParcelas(ApplicationUser user);
        Task<Parcela> ProcessarParcelasId(int id, ApplicationUser user);
        public Task<bool> AtualizarSaldoUsuario(double saldoTransacao, TipoEnum tipo, ApplicationUser user);
    }
}
