using Microsoft.AspNetCore.Mvc;
using PentaGol.Service.Interfaces;

namespace PentaGol.Api.Controllers;

public class GameController : BaseController
{
    private readonly IGameService gameService;

    public GameController(IGameService gameService)
    {
        this.gameService = gameService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await this.gameService.RetrieveByIdAsync(id));

}
