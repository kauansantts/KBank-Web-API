using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;

namespace KBank_Web_API.Service
{
    public interface ITransacaoService
    {
        public Task<Transacao> CancelarTransacao(int id);
        public Task<IEnumerable<Transacao>> GetTransacaoData(DateTime dataInicial, DateTime dataFinal);
        public Task<bool> AtualizarSaldoUsuario(double saldoTransacao, TipoEnum tipo, ApplicationUser user);
    }
}
