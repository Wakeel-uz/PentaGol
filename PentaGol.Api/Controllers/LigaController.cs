using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.DTOs.Teams;
using PentaGol.Service.Interfaces;
using PentaGol.Service.Services;

namespace PentaGol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LigaController : ControllerBase
    {
        private readonly ILigaService ligaService;

        public LigaController(ILigaService ligaService)
        {
            this.ligaService = ligaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
            => Ok(new
            {
                Code = 200,
                
            });

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]LigaForCreationDto liga)
        {
            return Ok(await this.ligaService.CreateAsync(liga));
        }
        [HttpPost("upload-image")]
        public async Task<IActionResult> ImageUpload([FromForm]LigaImageForCreationDto ligaImage)
        {
            return Ok(await this.ligaService.UploadImageAsync(ligaImage));
        }
        [HttpGet("teams-by-score/{LigaId}")]
        public async Task<IActionResult> RetrieveTeamsByScore(int LigaId)
        {
            return Ok(await this.ligaService.RetrieveTeamByLigaId(LigaId));
        }
        
    }

}
