using Microsoft.AspNetCore.Http;
using PentaGol.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace PentaGol.Service.DTOs.Ligas;

public class GameForCreationDto
{
    [Required(ErrorMessage = "Logo Image is required")]
    public IFormFile Image { get; set; }
    [Required(ErrorMessage = "Liga Name is required")]
    public string Name { get; set; }
    public ICollection<Game> Games { get; set; }
}
