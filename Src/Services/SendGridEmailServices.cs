using ProyectoDivine.Src.Services.interfaces;
using SendGrid.Helpers.Mail;

namespace ProyectoDivine.Src.Services;

public class SendGridEmailServices(IConfiguration configuration) : ISendGridEmailServices
{
    private readonly IConfiguration _configuration = configuration;
    /// <summary>
    /// Envía un correo electrónico utilizando SendGrid con una plantilla predefinida. 
    /// El correo se envía a la dirección especificada con el asunto y los datos de la función proporcionados. 
    /// La plantilla de SendGrid se utiliza para formatear el contenido del correo, incluyendo el nombre de la función y un string de validación. 
    /// El método devuelve un booleano indicando si el envío del correo fue exitoso o no, y maneja cualquier excepción que pueda ocurrir durante el proceso de envío.
    /// </summary>
    /// <param name="toEmail">Correo a enviar</param>
    /// <param name="subject">Asunto del correo</param>
    /// <param name="nombre_funcion">Nombre de la función</param>
    /// <param name="validacion_string">String de validación</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> SendEmailAsync(string toEmail, string subject, string nombre_funcion, string validacion_string)
    {
        try
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var fromEmail = _configuration["SendGrid:FromEmail"];
            var fromName = _configuration["SendGrid:FromName"];

            var client = new SendGrid.SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(toEmail);
            // Configurar el mensaje de correo electrónico utilizando una plantilla de SendGrid
            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.AddTo(to);
            // Establecer el ID de la plantilla de SendGrid que se utilizará para formatear el correo electrónico
            msg.SetTemplateId("d-c4fce420512e4c61a0ab0f026e107d25");
            // Configurar los datos de la plantilla con el nombre de la función y el string de validación
            msg.SetTemplateData(new
            {
                nombre_funcion = nombre_funcion,
                validacion_string = validacion_string
            });
            // Enviar el correo electrónico utilizando el cliente de SendGrid y registrar la respuesta
            var response = await client.SendEmailAsync(msg);
            // Registrar el resultado del envío del correo electrónico para depuración
            Console.WriteLine($"Correo electrónico enviado a {toEmail} con estado: {response.StatusCode} y {response.Body.ReadAsStringAsync().Result}");
            // Devolver true si el envío del correo electrónico fue exitoso (código de estado 2xx), de lo contrario, devolver false
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}");
            return false;
        }
        throw new NotImplementedException();
    }
};

    