using PentaGol.Domain.Entities;

namespace PentaGol.Service.DTOs.Ligas;
public class LigaForResultDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public LigaImageForResultDto Image { get; set; }
}
