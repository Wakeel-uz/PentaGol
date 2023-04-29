namespace PentaGol.Service.Interfaces;

public interface IImageService
{ 
    Task<string> SaveImageAsync(byte[] imageBytes, string fileName);
}

