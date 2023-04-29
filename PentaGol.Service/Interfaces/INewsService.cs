using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.News;

namespace PentaGol.Service.Interfaces;

public interface INewsService
{
    Task<News> CreateAsync(NewsForCreationDto dto);
    Task<News> ModifyAsync(NewsForCreationDto dto);
    Task<bool> RemoveAsync(int id);
    Task<News> RetrieveByIdAsync(int id);
    Task<IEnumerable<News>> RetrieveAllAsync();
}
