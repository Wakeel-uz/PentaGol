using System.ComponentModel.DataAnnotations;

namespace PentaGol.Service.DTOs;

public class AdminForLoginDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
