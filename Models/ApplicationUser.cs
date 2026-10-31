using Microsoft.AspNetCore.Identity;

namespace KBank_Web_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public decimal SaldoUser { get; set; } = 0;
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }
    }
}
