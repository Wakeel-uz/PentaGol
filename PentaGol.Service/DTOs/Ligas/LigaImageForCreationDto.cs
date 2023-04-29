using Microsoft.AspNetCore.Http;

namespace PentaGol.Service.DTOs.Ligas;

public class LigaImageForCreationDto
{
    public IFormFile Image { get; set; }
    public int LigaId { get; set; }
}
