using Microsoft.AspNetCore.Mvc;
using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.DTOs.Teams;
using PentaGol.Service.Interfaces;

namespace PentaGol.Api.Controllers;

public class LigaController : BaseController
{
    private readonly ILigaService ligaService;

    public LigaController(ILigaService ligaService)
    {
        this.ligaService = ligaService;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await this.ligaService.RetrieveByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm]LigaForCreationDto liga)
    {
        return Ok(await this.ligaService.CreateAsync(liga));
    }
    [HttpPost("upload-image")]
    public async Task<IActionResult> ImageUpload([FromForm]LigaImageForCreationDto ligaImage)
    {
        return Ok(await this.ligaService.UploadImageAsync(ligaImage));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
            => Ok(new
            {
                Code = 200,
                Message = "Success",
                Data = await this.ligaService.RetrieveByIdAsync(id)
            });

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] LigaForCreationDto liga)
        {
            return Ok(await this.ligaService.CreateAsync(liga));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(new
            {
                Code = 200,
                Message = "Success",
                Data = await this.ligaService.RetrieveAllAsync()
            });


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
            => Ok(new
            {
                Code = 200,
                Message = "Success",
                Data = await this.ligaService.RemoveAsync(Id)
            });

        [HttpPost("image-upload")]
        public async Task<IActionResult> ImageUpload([FromForm]LigaImageForCreationDto ligaImage)
        {
            return Ok(await this.ligaService.UploadImageAsync(ligaImage));
        }

        [HttpDelete("image-delete/{ligaId:int}")]
        public async Task<IActionResult> DeleteLigaImageAsync(int ligaId)
            => Ok(new
            {
                Code = 200,
                Message = "Success",
                Data = await this.ligaService.RemoveImageAsync(ligaId)
            });

        [HttpGet("image-get/{LigaId:int}")]
        public async Task<IActionResult> GetImageAsync(int ligaImageId)
            => Ok(new
            {
                Code = 200,
                Message = "Success",
                Data = await this.ligaService.RetrieveImageAsync(ligaImageId)
            });

        

        [HttpGet("teams-by-score/{LigaId}")]
        public async Task<IActionResult> RetrieveTeamsByScore(int LigaId)
        {
            return Ok(await this.ligaService.RetrieveTeamByLigaId(LigaId));
        }
        

    }
}
