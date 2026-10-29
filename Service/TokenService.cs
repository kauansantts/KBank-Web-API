using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KBank_Web_API.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public JwtSecurityToken GerarAccessToken(IEnumerable<Claim> claims, IConfiguration _config)
        {
            var tokenClaims = claims.ToList();
            tokenClaims.RemoveAll(c => c.Type == JwtRegisteredClaimNames.Jti);
            tokenClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var identidade = new ClaimsIdentity(tokenClaims);

            var key = _config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new ArgumentNullException("Chave secreta nao encontrada!");
            var secretKey = Encoding.UTF8.GetBytes(key);
            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
            var lifeTime = _config.GetSection("JWT").GetValue<int>("TokenValidatityInMinutes");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identidade,
                Audience = _config.GetSection("JWT").GetValue<string>("ValidAudience"),
                Issuer = _config.GetSection("JWT").GetValue<string>("ValidIssuer"),
                Expires = DateTime.UtcNow.AddMinutes(lifeTime),
                SigningCredentials = SigningCredentials
            };

            var token = new JwtSecurityTokenHandler().CreateJwtSecurityToken(tokenDescriptor);
            return token;
        }

        public string GerarRefreshToken()
        {
            var secureRandomBytes = new Byte[128];
            using var randomNumber = RandomNumberGenerator.Create();

            randomNumber.GetBytes(secureRandomBytes);

            var refreshToken = Convert.ToBase64String(secureRandomBytes);

            return refreshToken;
        }

        public ClaimsPrincipal GetPrincipalClaims(string token, IConfiguration _config)
        {
            var key = _config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new ArgumentNullException("Chave secreta nao encontrada!");

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }
    }
}
