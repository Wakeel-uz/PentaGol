using AutoMapper;
using PentaGol.Data.IRepositories;
using PentaGol.Domain.Entities;
using PentaGol.Domain.Entities.ImageEntities;
using PentaGol.Service.DTOs.News;
using PentaGol.Service.Exceptions;
using PentaGol.Service.Extensions;
using PentaGol.Service.Helpers;
using PentaGol.Service.Interfaces;

namespace PentaGol.Service.Services;

public class NewsService : INewsService
{
    private readonly IRepository<News> _newsRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<NewsImage> _newsImageRepository;

    public NewsService(
        IRepository<News> newsRepository, 
        IMapper mapper,
        IRepository<NewsImage> newsImageRepository)

    {
        this._newsRepository = newsRepository;
        this._mapper = mapper;
        this._newsImageRepository = newsImageRepository;
    }

    public async Task<News> CreateAsync(NewsForCreationDto dto)
    {
        News news = await this._newsRepository.SelectAsync(n => n.Title.ToLower() == dto.Title.ToLower());
        if (news is not null)
            throw new PentaGolException(403, "News already exists with this title");

        News mappedNews = _mapper.Map<News>(dto);
        var result = await this._newsRepository.InsertAsync(mappedNews);
        await this._newsRepository.SaveChangesAsync();
        return this._mapper.Map<News>(result);
    }
    #region Update News
    /// <summary>
    /// Modify method to edit
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<News> ModifyAsync(NewsForCreationDto dto)
    {
        var modifyingNews = await this._newsImageRepository.SelectAsync(l => l.Id.Equals(dto.));
        if (modifyingNews is null)
            throw new PentaGolException(404, "News couldn't be found");

        this._mapper.Map(dto, modifyingNews);
        modifyingNews.UpdatedAt = DateTime.UtcNow;
        await this._newsImageRepository.SaveChangesAsync();

        var result = _mapper.Map<News>(modifyingNews);
        result.Image = _mapper.Map<NewsImageForResultDto>(
            await this._newsImageRepository.SelectAsync(l => l.NewsId.Equals(result.Id)));
        return result;

    }

    #endregion

    public async Task<bool> RemoveAsync(int id)
    {
        var news = await this._newsRepository.SelectAsync(n => n.Id.Equals(id));
        if(news is null)
        {
            throw new PentaGolException(404, " News not found");
        }
        await this._newsRepository.DeleteAsync(news);
        await this._newsRepository.SaveChangesAsync();
        return true;
    }

    #region GetAll News
    /// <summary>
    /// Fetching all News
    /// </summary>
    /// <returns>IEnumerable</returns>
    public async Task<IEnumerable<News>> RetrieveAllAsync()
    {
        var news = _newsRepository.SelectAll(isTracking: false);
        var result = _mapper.Map<IEnumerable<News>>(news);

        foreach (var item in result)
            item.Image = _mapper.Map<News>(
                await this._newsImageRepository.SelectAsync(l => l.NewsId.Equals(item.Id)));
        return result;
    }

    #endregion

    #region GetById News
    /// <summary>
    /// Fetching News by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>One Liga</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<News> RetrieveByIdAsync(int id)
    {
        var news = await this._newsRepository.SelectAsync(l => l.Id.Equals(id));
        if (news is null)
            throw new PentaGolException(404, "News couldn't be found");

        var result = _mapper.Map<News>(news);
        result.Image = _mapper.Map<NewsImageForResultDto>(
            await this._newsImageRepository.SelectAsync(l => l.Id.Equals(result.Id)));

        return result;
    }

    #endregion

    #region Get Image
    /// <summary>
    /// Fetching Image
    /// </summary>
    /// <param name="newsId"></param>
    /// <returns>One Image</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<NewsImageForResultDto> RetrieveImageAsync(int newsId)
    {
        var newsImage = await this._newsImageRepository.SelectAsync(t => t.NewsId.Equals(newsId));
        if (newsImage is null)
            throw new PentaGolException(404, "Image is not found");
        return _mapper.Map<NewsImageForResultDto>(newsImage);
    }

    #endregion


    #region Remove Image
    /// <summary>
    /// Disposing an image
    /// </summary>
    /// <param name="NewsId"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<bool> RemoveImageAsync(int newsId)
    {
        var newsImage = await this._newsImageRepository.SelectAsync(t => t.NewsId.Equals(newsId));
        if (newsImage is null)
            throw new PentaGolException(404, "Image is not found");

        File.Delete(newsImage.Path);
        await this._newsImageRepository.DeleteAsync(newsImage);
        await this._newsImageRepository.SaveChangesAsync();
        return true;
    }
    #endregion
    #region Upload Image
    /// <summary>
    /// Uploading Image from a source (e.g Laptop or Computer)
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Image</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<NewsImageForResultDto> UploadImageAsync(NewsImageForCreationDto dto)
    {
        var news = await this._newsRepository.SelectAsync(t => t.Id.Equals(dto.NewsId));
        if (news is null)
            throw new PentaGolException(404, "News is not found");

        byte[] image = dto.Image.ToByteArray();
        var fileExtension = Path.GetExtension(dto.Image.FileName);
        var fileName = Guid.NewGuid().ToString("N") + fileExtension;
        var webRootPath = EnvironmentHelper.WebHostPath;
        var folder = Path.Combine(webRootPath, "uploads", "images");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fullPath = Path.Combine(folder, fileName);
        using var imageStream = new MemoryStream(image);

        using var imagePath = new FileStream(fullPath, FileMode.CreateNew);
        imageStream.WriteTo(imagePath);

        var newsImage = new NewsImage
        {
            Name = fileName,
            Path = fullPath,
            NewsId = dto.NewsId,
            News = news,
            CreatedAt = DateTime.UtcNow,
        };

        var createdImage = await this._newsImageRepository.InsertAsync(newsImage);
        await this._newsImageRepository.SaveChangesAsync();
        return _mapper.Map<NewsImageForResultDto>(createdImage);
    }
}
#endregion
