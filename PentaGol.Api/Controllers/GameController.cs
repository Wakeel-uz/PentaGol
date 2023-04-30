using Microsoft.AspNetCore.Mvc;
using PentaGol.Service.DTOs.Games;
using PentaGol.Service.Interfaces;

namespace PentaGol.Api.Controllers;

public class GameController : BaseController
{
    private readonly IGameService gameService;

    public GameController(IGameService gameService)
    {
        this.gameService = gameService;
    }

    [HttpPost]
    public async Task<ActionResult<GameForResultDto>> CreateNewsAsync(GameForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.gameService.CreateAsync(dto)
        });

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteNewsAsync(int id)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.gameService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.gameService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-list")]
    public async Task<IActionResult> GetAllGames()
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.gameService.RetrieveAllAsync()
        });
}
