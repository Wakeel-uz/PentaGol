using PentaGol.Service.DTOs.Teams;

namespace PentaGol.Service.Interfaces;

public interface ITeamService
{
    Task<TeamForResultDto> CreateAsync(TeamForCreationDto dto);
    Task<TeamForResultDto> ModifyAsync(TeamForCreationDto dto);
    Task<TeamForResultDto> RetrieveById(int id);
    Task<bool> RemoveAsync(int id);
    Task<IEnumerable<TeamForResultDto>> RetrieveAllAsync();
}
