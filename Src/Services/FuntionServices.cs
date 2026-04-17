using Microsoft.EntityFrameworkCore;
using ProyectoDivine.Src.Dtos.Funtion;
using ProyectoDivine.Src.Services.interfaces;

namespace ProyectoDivine.Src.Services;

public class FuntionServices(ICloudinaryServices cloudinaryServices, DbContext dbContext) : IFuntionServices
{
    public readonly DbContext _dbContext = dbContext;
    public readonly ICloudinaryServices _cloudinaryServices = cloudinaryServices;
    public async Task<CreateFuntionResponse> CreateFuntionAsync(CreateFuntion request)
    {
        string ResponseImageUrl = string.Empty;
        try
        {
            if(request.Image != null)
            {
                var imageUrl = await _cloudinaryServices.UploadImageAsync(request.Image, "functions");
                if(imageUrl == null)
                {
                    return new CreateFuntionResponse
                    {
                        Message = "Error al subir la imagen",
                        Success = false,
                        Data = null!
                    };
                }
                ResponseImageUrl = imageUrl;
            }
            else
            {
                ResponseImageUrl = string.Empty;
            }
            var funtion = new CreateFuntionData
            {
                
                Name = request.Name,
                Description = request.Description,
                DateFunction = request.DateFunction,
                TimeFunction = request.TimeFunction,
                State = true, // Asumiendo que la función se crea en estado activo
                ImageUrl = ResponseImageUrl
            };
            await _dbContext.AddAsync(funtion);
            await _dbContext.SaveChangesAsync();
            return new CreateFuntionResponse
            {
                Message = "Función creada exitosamente",
                Success = true,
                Data = funtion
            };
        }
        catch (Exception ex)
        {
            return new CreateFuntionResponse
            {
                Message = $"Error al crear la función: {ex.Message}",
                Success = false,
                Data = null!
            };
        }
        throw new NotImplementedException();
    }
}