using PentaGol.Service.Interfaces;

namespace PentaGol.Service.Services;

public class ImageService : IImageService
{
    private readonly string _basePath;

    public ImageService(string basePath)
    {
        _basePath = basePath;
    }

    public async Task<string> SaveImageAsync(byte[] imageBytes, string fileName)
    {
        var filePath = Path.Combine(_basePath, fileName);
        await File.WriteAllBytesAsync(filePath, imageBytes);
        return filePath;
    }
}
