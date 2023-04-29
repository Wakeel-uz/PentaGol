using PentaGol.Service.DTOs;
using PentaGol.Service.Interfaces;

namespace PentaGol.Service.Services;

public class LigaService : ILigaService
{
    public Task<LigaForResultDto> CreateAsync(LigaForCreationDto dto)
    {
        
    }

    public Task<LigaForResultDto> ModifyAsync(LigaForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LigaForResultDto>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<LigaForResultDto> RetrieveByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
