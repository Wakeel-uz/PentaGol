﻿namespace PentaGol.Service.DTOs.Teams;

public class TeamForResultDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LigaId { get; set; }
    public int TotalGame { get; set; }
    public int TotalScore { get; set; }
    public TeamImageForResultDto Image { get; set; }
}
