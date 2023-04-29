using PentaGol.Service.DTOs.Ligas;

namespace PentaGol.Service.Interfaces;

public interface ILigaService
{
    Task<LigaForResultDto> CreateAsync(LigaForCreationDto dto);
    Task<LigaForResultDto> ModifyAsync(LigaForCreationDto dto);
    Task<LigaForResultDto> RetrieveByIdAsync (int id);
    Task<bool> RemoveAsync (int id);
    Task<IEnumerable<LigaForResultDto>> RetrieveAllAsync ();
    Task<LigaImageForResultDto> UploadImageAsync(LigaImageForCreationDto dto);
    Task<LigaImageForResultDto> RetrieveImageAsync(int ligaId);
    Task<bool> RemoveImageAsync(int ligaId);
}
