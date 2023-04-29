using PentaGol.Service.DTOs.Games;
using PentaGol.Service.Interfaces;

namespace PentaGol.Service.Services;

public class GameService : IGameService
{
    public Task<GameForResultDto> CreateAsync(GameForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<GameForResultDto> ModifyAsync(GameForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GameForResultDto>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GameForResultDto> RetrieveByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
