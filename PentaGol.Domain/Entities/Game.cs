using PentaGol.Domain.Commons;

namespace PentaGol.Domain.Entities;

public class Game : Auditable
{
    public DateTime GameDate { get; set; }
    public int FirstTeamId { get; set; }
    public int SecondTeamId { get; set; }
    public int FirstTeamScore { get; set; }
    public int SecondTeamScore { get; set; }
    public DateTime StartingTime { get; set; }
    
    //To determine whether the game has finished or not 
    public bool IsFinished { get; set; }

}
