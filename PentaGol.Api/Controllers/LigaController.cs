using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Ligas;
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
            => Ok(await this.ligaService.RetrieveByIdAsync(id));

        [HttpPost("liga")]
        public async Task<IActionResult> CreateAsync([FromBody]LigaForCreationDto liga)
        {
            return Ok(await this.ligaService.CreateAsync(liga));
        }
    }

}
