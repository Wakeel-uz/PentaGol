using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.DTOs.News;

namespace PentaGol.Service.Interfaces;

public interface INewsService
{
    Task<NewsForResultDto> CreateAsync(NewsForCreationDto dto);
    Task<NewsForResultDto> ModifyAsync(News dto);
    Task<bool> RemoveAsync(int id);
    Task<NewsForResultDto> RetrieveByIdAsync(int id);
    Task<IEnumerable<NewsForResultDto>> RetrieveAllAsync();
    Task<NewsImageForResultDto> UploadImageAsync(NewsImageForCreationDto dto);
    Task<NewsImageForResultDto> RetrieveImageAsync(int newsId);
    Task<bool> RemoveImageAsync(int newsId);
}
