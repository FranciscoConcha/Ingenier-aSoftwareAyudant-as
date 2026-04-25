using ProyectoDivine.Src.Services.interfaces;
using SendGrid.Helpers.Mail;

namespace ProyectoDivine.Src.Services;

public class SendGridEmailServices(IConfiguration configuration) : ISendGridEmailServices
{
    private readonly IConfiguration _configuration = configuration;

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

            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.AddTo(to);

            msg.SetTemplateId("d-c4fce420512e4c61a0ab0f026e107d25");

            msg.SetTemplateData(new
            {
                nombre_funcion = nombre_funcion,
                validacion_string = validacion_string
            });
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine($"Correo electrónico enviado a {toEmail} con estado: {response.StatusCode} y {response.Body.ReadAsStringAsync().Result}");
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

    