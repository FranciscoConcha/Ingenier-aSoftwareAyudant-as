using ProyectoDivine.Src.Db;
using ProyectoDivine.Src.Dtos.Funtion;
using ProyectoDivine.Src.Services.interfaces;
using ProyectoDivine.Src.Model;
using System;
namespace ProyectoDivine.Src.Services;

public class FuntionServices(ICloudinaryServices cloudinaryServices, ContextDb contextDb) : IFuntionServices
{
    public readonly ContextDb _contextDb = contextDb;
    public readonly ICloudinaryServices _cloudinaryServices = cloudinaryServices;
    public async Task<CreateFuntionResponse> CreateFuntionAsync(CreateFuntion request)
    {
        string ResponseImageUrl = string.Empty;
        Guid miUuid = Guid.NewGuid();
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
            var funtion = new Funtion
            {
                Name = request.Name,
                Description = request.Description,
                DateFunction = request.DateFunction,
                TimeFunction = request.TimeFunction,
                State = false,
                Image = ResponseImageUrl,
                // Generar un UUID compacto sin guiones para la validación de la función
                ValidateFuntion = miUuid.ToString("N")
            };
            await _contextDb.Functions.AddAsync(funtion);
            await _contextDb.SaveChangesAsync();
            return new CreateFuntionResponse
            {
                Message = "Función creada exitosamente",
                Success = true,
                Data = new CreateFuntionData
                {
                    Id = funtion.Id,
                    Name = funtion.Name,
                    Description = funtion.Description,
                    DateFunction = funtion.DateFunction,
                    TimeFunction = funtion.TimeFunction,
                    State = funtion.State,
                    ImageUrl = funtion.Image ?? string.Empty
                }
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
    }
}

