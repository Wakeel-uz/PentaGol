using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.DTOs.Teams;

namespace PentaGol.Service.Interfaces;

public interface ITeamService
{
    Task<TeamForResultDto> CreateAsync(TeamForCreationDto dto);
    Task<TeamForResultDto> ModifyAsync(TeamForCreationDto dto);
    Task<TeamForResultDto> RetrieveById(int id);
    Task<bool> RemoveAsync(int id);
    Task<IEnumerable<TeamForResultDto>> RetrieveAllAsync();
    Task<TeamImageForResultDto> UploadImageAsync(TeamImageForCreationDto dto);
    Task<TeamImageForResultDto> RetrieveImageAsync(int teamId);
    Task<bool> RemoveImageAsync(int teamId);
}
