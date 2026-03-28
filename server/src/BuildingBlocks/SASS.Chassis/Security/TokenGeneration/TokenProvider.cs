using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace SASS.Chassis.Security.TokenGeneration;

internal sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string Create(Guid userId, string email)
    {
        string secretKey = configuration["JwtOptions:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtOptions:ExpiredAccessToken")),
            SigningCredentials = credentials,
            Issuer = configuration["JwtOptions:Issuer"],
            Audience = configuration["JwtOptions:Audience"]
        };
        
        var handler = new JsonWebTokenHandler();
        
        string token = handler.CreateToken(tokenDescriptor);
        
        return token;
    }
}
