namespace ProyectoDivine.Src.Services.interfaces;

public interface ISendGridEmailServices
{
    Task<bool> SendEmailAsync(string toEmail, string subject, string nombre_funcion, string validacion_string);
}