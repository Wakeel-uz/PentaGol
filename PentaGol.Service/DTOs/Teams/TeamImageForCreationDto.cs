using Microsoft.AspNetCore.Http;

namespace PentaGol.Service.DTOs.Teams;

public class TeamImageForCreationDto
{
    public IFormFile Image { get; set; }
    public int TeamId { get; set; }
}
