using PentaGol.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PentaGol.Service.DTOs;

public class TeamForCreationDto
{
    [Required(ErrorMessage = "Image is required")]
    public string ImagePath { get; set; }
    [Required(ErrorMessage = "Club name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Liga Id is required")]
    public int LigaId { get; set; }
    
}
