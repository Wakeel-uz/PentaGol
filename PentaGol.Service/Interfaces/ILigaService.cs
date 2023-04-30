using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.DTOs.Teams;

namespace PentaGol.Service.Interfaces;

public interface ILigaService
{
    Task<LigaForResultDto> CreateAsync(LigaForCreationDto dto);
    Task<LigaForResultDto> ModifyAsync(Liga dto);
    Task<LigaForResultDto> RetrieveByIdAsync (int id);
    Task<bool> RemoveAsync (int id);
    Task<IEnumerable<LigaForResultDto>> RetrieveAllAsync ();
    Task<LigaImageForResultDto> UploadImageAsync(LigaImageForCreationDto dto);
    Task<bool> RemoveImageAsync (int ligaId);
    Task<LigaImageForResultDto> RetrieveImageAsync(int ligaId);
    Task<List<TeamForResultDto>> RetrieveTeamByLigaId(int ligaId);
}
