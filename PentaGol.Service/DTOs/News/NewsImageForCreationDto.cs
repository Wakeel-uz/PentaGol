
using Microsoft.AspNetCore.Http;

namespace PentaGol.Service.DTOs.News;

public class NewsImageForCreationDto
{
    public IFormFile Image {get; set; }
    public int LigaId { get; set; }
}
