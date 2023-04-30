using PentaGol.Service.DTOs;

namespace PentaGol.Service.Interfaces;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string username, string password);
}
