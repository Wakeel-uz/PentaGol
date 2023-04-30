using Microsoft.AspNetCore.Mvc;
using PentaGol.Service.DTOs.News;
using PentaGol.Service.DTOs.Teams;
using PentaGol.Service.Interfaces;

namespace PentaGol.Api.Controllers;

public class TeamController : BaseController
{
    private readonly ITeamService teamService;

    public TeamController(ITeamService teamService)
    {
        this.teamService = teamService;
    }

    [HttpPost]
    public async Task<ActionResult<TeamForResultDto>> CreateNewsAsync(TeamForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.teamService.CreateAsync(dto)
        });

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteTeamAsync(int id)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.teamService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.teamService.RetrieveById(id)
        });

    [HttpGet("get-list")]
    public async Task<IActionResult> GetAll()
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.teamService.RetrieveAllAsync()
        });

    [HttpPost("upload-image")]
    public async ValueTask<IActionResult> UploadImage([FromForm] TeamImageForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.teamService.UploadImageAsync(dto)
        });

    [HttpDelete("delete-image/{teamId:int}")]
    public async Task<IActionResult> DeleteImage(int teamId)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.teamService.RemoveImageAsync(teamId)
        });

    [HttpGet("get-image/{teamId:int}")]
    public async Task<IActionResult> GetImage(int teamId)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.teamService.RetrieveImageAsync(teamId)
        });
}
