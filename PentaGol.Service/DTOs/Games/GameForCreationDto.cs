using System.ComponentModel.DataAnnotations;

namespace PentaGol.Service.DTOs.Games;

public class GameForCreationDto
{
    public DateTime GameDate { get; set; }
    [Required(ErrorMessage = "FirstTeamId is required")]
    public int FirstTeamId { get; set; }
    [Required(ErrorMessage = "SecondTeamId is required")]
    public int SecondTeamId { get; set; }
    public DateTime StartingTime { get; set; }
    public int DurationInMinutes { get; set; }
    public int? FirstTeamScore { get; set; }
    public int? SecondTeamScore { get; set; }

}
