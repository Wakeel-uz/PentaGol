using System.ComponentModel.DataAnnotations;

namespace PentaGol.Service.DTOs.Games;

public class GameForCreationDto
{
    [Required(ErrorMessage = "FirstTeamId is required")]
    public int FirstTeamId { get; set; }
    [Required(ErrorMessage = "SecondTeamId is required")]
    public int SecondTeamId { get; set; }
    [Required(ErrorMessage = "FirstTeamScore is required")]
    public int FirstTeamScore { get; set; }
    [Required(ErrorMessage = "SecondTeamScore is required")]
    public int SecondTeamScore { get; set; }
    public DateTime StartingDate { get; set; }
    public bool IsFinished { get; set; }
}
