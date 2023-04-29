using PentaGol.Domain.Commons;

namespace PentaGol.Domain.Entities;

public class New : Auditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
