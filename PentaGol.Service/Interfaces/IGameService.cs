using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Games;

namespace PentaGol.Service.Interfaces;

public interface IGameService
{
    Task<GameForResultDto> CreateAsync(GameForCreationDto dto);
    Task<GameForResultDto> ModifyAsync(Game dto);
    Task<bool> RemoveAsync(int id);
    Task<GameForResultDto> RetrieveByIdAsync(int id);
    Task<IEnumerable<GameForResultDto>> RetrieveAllAsync();
    
}
