using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KBank_Web_API.Service
{
    public interface ITokenService
    {
        JwtSecurityToken GerarAccessToken(IEnumerable<Claim> claims, IConfiguration _config);
        string GerarRefreshToken();
        ClaimsPrincipal GetPrincipalClaims(string token, IConfiguration _config);
    }
}
