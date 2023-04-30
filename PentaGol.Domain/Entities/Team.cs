using PentaGol.Domain.Commons;

namespace PentaGol.Domain.Entities;

public class Team : Auditable
{
    public string ImagePath { get; set; }
    public string Name { get; set; }
    public int LigaId { get; set; }
    public Liga Liga { get; set; }
    public int TotalGame { get; set; }
    public int TotalScoredGoals { get; set; }
    public int TotalReceivedGoals { get; set; }
    public int TotalScore { get; set; }
}
