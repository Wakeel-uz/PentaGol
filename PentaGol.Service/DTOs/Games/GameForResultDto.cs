using PentaGol.Domain.Commons;

namespace PentaGol.Service.DTOs.Games;

public class GameForResultDto
{
    public int Id { get; set; }
    public DateTime GameDate { get; set; }
    public int FirstTeamId { get; set; }
    public int SecondTeamId { get; set; }
    public int FirstTeamScore { get; set; }
    public int SecondTeamScore { get; set; }
    public DateTime StartingTime { get; set; }
    public int DurationInMinutes { get; set; }
    public bool IsFinished { get; set; }
}
