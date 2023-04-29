using System.ComponentModel.DataAnnotations;

namespace PentaGol.Service.DTOs; 

public class NewsForCreationDto 
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "ImagePath is required")]
    public string ImagePath { get; set; }
}
