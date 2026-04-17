using ProyectoDivine.Src.Dtos.Funtion;
namespace ProyectoDivine.Src.Services.interfaces;

public interface IFuntionServices
{
    Task<CreateFuntionResponse> CreateFuntionAsync(CreateFuntion request);
}