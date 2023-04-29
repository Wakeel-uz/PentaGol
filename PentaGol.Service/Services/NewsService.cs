using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.News;
using PentaGol.Service.Interfaces;

namespace PentaGol.Service.Services;

public class NewsService : INewsService
{
    public Task<News> CreateAsync(NewsForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<News> ModifyAsync(NewsForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<News>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<News> RetrieveByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
