using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;

namespace KBank_Web_API.Service
{
    public interface ITransacaoService
    {
        public Task<Transacao> CancelarTransacao(int id, ApplicationUser user);
        public Task<IEnumerable<Transacao>> GetTransacaoData(DateTime dataInicial, DateTime dataFinal, ApplicationUser user);
        public Task<bool> AtualizarSaldoUsuario(double saldoTransacao, TipoEnum tipo, ApplicationUser user);
    }
}
