using PentaGol.Domain.Commons;
using System.Runtime.CompilerServices;

namespace PentaGol.Domain.Entities.ImageEntities;

public class TeamImage : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
}
