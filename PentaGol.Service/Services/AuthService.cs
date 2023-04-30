using Microsoft.IdentityModel.Tokens;
using PentaGol.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PentaGol.Service.Services;

#pragma warning disable
public class AuthService : IAuthService
{
    private readonly string _adminUsername = "admin";
    private readonly string _adminPassword = "admin";

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        if (username == _adminUsername && password == _adminPassword)
        {
            // Generate and return a JWT token for the authenticated admin
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your_jwt_secret_key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        else
        {
            // Return null for authentication failure
            return null;
        }
    }
}
