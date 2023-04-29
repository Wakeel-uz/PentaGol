using PentaGol.Service.DTOs;

namespace PentaGol.Service.Interfaces;

public interface ILigaService
{
    Task<LigaForResultDto> CreateAsync(LigaForCreationDto dto);
    Task<LigaForResultDto> ModifyAsync(LigaForCreationDto dto);
    Task<LigaForResultDto> RetrieveByIdAsync (int id);
    Task<bool> RemoveAsync (int id);
    Task<IEnumerable<LigaForResultDto>> RetrieveAllAsync ();
}
