using Microsoft.AspNetCore.Mvc;
using PentaGol.Service.DTOs;
using PentaGol.Service.Interfaces;

namespace PentaGol.Api.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("admin/login")]
    public IActionResult AdminLogin([FromBody] AdminForLoginDto dto)
    {
        var token = _authService.AuthenticateAsync(dto.Username, dto.Password);

        if (token != null)
        {
            // admin login successful, return the JWT token
            return Ok(new { token });
        }
        else
        {
            // admin login failed, return an error response
            return BadRequest("Invalid username or password.");
        }
    }
}
