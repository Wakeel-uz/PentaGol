using PentaGol.Service.DTOs.Ligas;

namespace PentaGol.Service.Interfaces;

public interface ILigaService
{
    Task<LigaForResultDto> CreateAsync(GameForCreationDto dto);
    Task<LigaForResultDto> ModifyAsync(GameForCreationDto dto);
    Task<LigaForResultDto> RetrieveByIdAsync (int id);
    Task<bool> RemoveAsync (int id);
    Task<IEnumerable<LigaForResultDto>> RetrieveAllAsync ();
}
