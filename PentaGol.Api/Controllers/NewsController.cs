using Microsoft.AspNetCore.Mvc;
using PentaGol.Service.DTOs.News;
using PentaGol.Service.Interfaces;

namespace PentaGol.Api.Controllers;

public class NewsController : BaseController
{
    private readonly INewsService newsService;

    public NewsController(INewsService newsService)
    {
        this.newsService = newsService;
    }

    [HttpPost]
    public async Task<ActionResult<NewsForResultDto>> CreateNewsAsync(NewsForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.newsService.CreateAsync(dto)
        });

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteNewsAsync(int id)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.newsService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.newsService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-list")]
    public async Task<IActionResult> GetAllNews()
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.newsService.RetrieveAllAsync()
        });

    [HttpPost("upload-image")]
    public async ValueTask<IActionResult> UploadImage([FromForm] NewsImageForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.newsService.UploadImageAsync(dto)
        });

    [HttpDelete("delete-image/{newsId:int}")]
    public async Task<IActionResult> DeleteImage(int newsId)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.newsService.RemoveImageAsync(newsId)
        });

    [HttpGet("get-image/{newsId:int}")]
    public async Task<IActionResult> GetImage(int newsId)
        => Ok(new
        {
            Code = 200,
            Message = "Success",
            Data = await this.newsService.RetrieveImageAsync(newsId)
        });

}
