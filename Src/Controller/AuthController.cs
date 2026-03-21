using Microsoft.AspNetCore.Mvc;
using ProyectoDivine.Src.Dtos.Users;
using ProyectoDivine.Src.Services.interfaces;

namespace ProyectoDivine.Src.Controller;
/// <summary>
/// Controlador para manejar las solicitudes de autenticación de usuarios en la aplicación.
/// Contiene un método Login que recibe las credenciales del usuario a través de un objeto LoginDto, 
/// y utiliza el servicio de autenticación (IAuthServices) para procesar la solicitud de inicio de sesión.
/// El método Login devuelve una respuesta HTTP que indica si el inicio de sesión fue exitoso o no, 
/// junto con un mensaje descriptivo y los datos relevantes del usuario autenticado en caso de éxito.
/// [ApiController] indica que esta clase es un controlador de API, 
/// lo que permite que el framework maneje automáticamente la validación de modelos y la generación de respuestas HTTP adecuadas.
/// [Route("api/[controller]")] define la ruta base para las solicitudes a este controlador, 
/// donde [controller] se reemplaza por el nombre del controlador (en este caso, "Auth"), 
/// lo que significa que las solicitudes a este controlador deben dirigirse a "api/auth".
/// El constructor del controlador recibe una instancia de IAuthServices a través de la inyección de dependencias,
/// lo que permite que el controlador utilice los métodos definidos en la interfaz para manejar la lógica de autenticación 
/// sin acoplarse directamente a una implementación específica del servicio de autenticación.
/// </summary>
/// <param name="authServices">La instancia del servicio de autenticación.</param>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthServices authServices) : ControllerBase
{
    /// Campo privado para almacenar la instancia del servicio de autenticación, 
    /// que es inyectada a través del constructor del controlador.
    private readonly IAuthServices _authServices = authServices;
    /// Método para manejar las solicitudes de inicio de sesión de los usuarios.
    /// Recibe un objeto LoginDto con las credenciales del usuario a través del cuerpo de la solicitud HTTP, 
    /// y utiliza el servicio de autenticación para procesar la solicitud.
    /// [FromBody] indica que el objeto LoginDto se debe deserializar a partir del cuerpo de la solicitud HTTP,
    /// lo que permite que el cliente envíe las credenciales del usuario en formato JSON
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var response = await _authServices.Login(loginDto);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}