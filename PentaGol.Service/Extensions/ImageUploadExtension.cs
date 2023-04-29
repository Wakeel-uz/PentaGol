using Microsoft.AspNetCore.Http;


namespace PentaGol.Service.Extensions;

public static class ImageUploadExtension
{
    public static byte[] ToByteArray(this IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        using var fileStream = file.OpenReadStream();
        fileStream.Seek(0, SeekOrigin.Begin);
        fileStream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}
