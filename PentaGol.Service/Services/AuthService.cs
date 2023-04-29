using PentaGol.Service.Interfaces;

namespace PentaGol.Service.Services;

public class AuthService : IAuthService
{
    public Task<string> GenerateTokenAsync(string username, string password)
    {
        throw new NotImplementedException();
    }
}
