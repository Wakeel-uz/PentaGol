using PentaGol.Domain.Commons;

namespace PentaGol.Domain.Entities;

public class Liga : Auditable
{
    public string LogoPath { get; set; }
    public string Name { get; set; }
    public ICollection<Game> Games { get; set; }
}
