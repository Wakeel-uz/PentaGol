using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaGol.Service.DTOs.News
{
    public class NewsForResultDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
  
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }

        public NewsImageForResultDto Image { get; set; }
    }
}
