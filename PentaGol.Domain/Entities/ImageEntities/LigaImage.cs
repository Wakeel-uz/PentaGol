using PentaGol.Domain.Commons;

namespace PentaGol.Domain.Entities.ImageEntities;   
public class LigaImage : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public int LigaId { get; set; }
    public Liga Liga { get; set; }
}
