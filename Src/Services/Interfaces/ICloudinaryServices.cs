namespace ProyectoDivine.Src.Services.interfaces;

public interface ICloudinaryServices
{
    Task<string?> UploadImageAsync(IFormFile file, string folder);
    
}