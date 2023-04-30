using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Ligas;

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
}
