using PentaGol.Domain.Commons;

namespace PentaGol.Domain.Entities;

public class Game : Auditable
{
    public DateTime GameDate { get; set; }
    public int FirstTeamId { get; set; }
    public int SecondTeamId { get; set; }
    public int FirstTeamScore { get; set; }
    public int SecondTeamScore { get; set; }
}
