using PentaGol.Domain.Commons;

namespace PentaGol.Domain.Entities.ImageEntities;

public class NewsImage : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public int NewsId { get; set; }
    public News News { get; set; }

}
