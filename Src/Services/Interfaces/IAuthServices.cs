using ProyectoDivine.Src.Dtos.Users;

namespace ProyectoDivine.Src.Services.interfaces;
/// <summary>
/// Interfaz que define los métodos para la autenticación de usuarios en la aplicación.
/// Contiene un método Login que recibe un objeto LoginDto con las credenciales del usuario y 
/// devuelve un objeto LoginResponse con el resultado del intento de inicio de sesión,
/// </summary>
public interface IAuthServices
{
    /// <summary>
    /// Método para autenticar a un usuario en la aplicación. Recibe un objeto LoginDto 
    /// que contiene el correo electrónico y la contraseña del usuario,
    /// y devuelve un objeto LoginResponse que indica si el inicio de sesión fue exitoso o no, 
    /// junto con un mensaje descriptivo y los datos relevantes del usuario autenticado en caso de éxito.
    /// </summary>
    /// <param name="loginDto">
    /// El objeto LoginDto que contiene las credenciales del usuario.
    /// </param>
    /// <returns>
    /// Retorna un objeto LoginResponse que indica el resultado del intento de inicio de sesión,
    /// incluyendo un mensaje descriptivo y los datos del usuario autenticado en caso de éxito.
    /// </returns>
    Task<LoginResponse> Login(LoginDto loginDto);
}