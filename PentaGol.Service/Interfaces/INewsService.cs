using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.DTOs.News;

namespace PentaGol.Service.Interfaces;

public interface INewsService
{
    Task<News> CreateAsync(NewsForCreationDto dto);
    Task<News> ModifyAsync(NewsForCreationDto dto);
    Task<bool> RemoveAsync(int id);
    Task<News> RetrieveByIdAsync(int id);
    Task<IEnumerable<News>> RetrieveAllAsync();
    Task<NewsImageForResultDto> UploadImageAsync(NewsImageForCreationDto dto);
    Task<NewsImageForResultDto> RetrieveImageAsync(int newsId);
    Task<bool> RemoveImageAsync(int newsId);
}
