using ProyectoDivine.Src.Services.interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;


namespace ProyectoDivine.Src.Services;

public class CloudinaryServices : ICloudinaryServices
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    /// <summary>
    /// Constructor de la clase CloudinaryServices que inicializa la instancia de Cloudinary utilizando las credenciales proporcionadas en la configuración de la aplicación.
    /// Obtiene el nombre de la nube, la clave de API y el secreto de API desde la configuración, y luego crea una instancia de Cloudinary con estas credenciales para permitir la interacción con el servicio de Cloudinary para subir imágenes.
    /// </summary>
    /// <param name="configuration">Configuración de la aplicación</param>
    public CloudinaryServices(IConfiguration configuration)
    {
        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];
        
        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }
    /// <summary>
    /// Método que permite subir una imagen a Cloudinary de forma asíncrona.
    /// </summary>
    /// <param name="file">Archivo de imagen a subir</param>
    /// <param name="folder">Carpeta donde se guardará la imagen</param>
    /// <returns>URL segura de la imagen subida o null en caso de error</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<string?> UploadImageAsync(IFormFile file, string folder)
    {
        if(file == null || file.Length == 0)
            {
                return null;
            }
        try
        {
            using var stream = file.OpenReadStream();
            // Configuración de los parámetros de subida para Cloudinary, incluyendo el archivo y la carpeta de destino.
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Folder = $"ISAyudantia/divine-teatro/{folder}"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if(uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return null;

        }catch (Exception ex)
        {
            
            Console.WriteLine($"Error al subir la imagen: {ex.Message}");
            return null;
        }
        throw new NotImplementedException();
    }
}