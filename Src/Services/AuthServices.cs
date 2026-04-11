using Microsoft.EntityFrameworkCore;
using ProyectoDivine.Src.Db;
using ProyectoDivine.Src.Dtos.Users;
using ProyectoDivine.Src.Services.interfaces;
using ProyectoDivine.Src.Utils;

namespace ProyectoDivine.Src.Services;
/// <summary>
/// Clase que implementa la interfaz IAuthServices para manejar la autenticación de usuarios en la aplicación.
/// Contiene un constructor que recibe el contexto de la base de datos y la configuración de la aplicación,
///  y un método Login que actualmente no ha sido implementado y lanza una excepción NotImplementedException.
/// </summary>
/// <param name="contextDb">El contexto de la base de datos.</param>
/// <param name="config">La configuración de la aplicación.</param>
public class AuthServices(ContextDb contextDb, IConfiguration config) : IAuthServices
{
    /// <summary>
    /// Campos privados para almacenar el contexto de la base de datos y la configuración de la aplicación,
    /// que son inyectados a través del constructor de la clase.
    /// Estos campos se utilizan para acceder a la base de datos y a la configuración de la aplicación en los métodos de autenticación.
    /// </summary>
    private readonly ContextDb _contextDb = contextDb;
    /// <summary>
    /// Campo privado para almacenar la configuración de la aplicación, que es inyectada a través del constructor de la clase.
    /// Este campo se utiliza para acceder a la configuración de la aplicación, como la clave de JWT, el emisor, la audiencia y otros parámetros relacionados con la autenticación y generación de tokens.  
    /// </summary>
    private readonly IConfiguration _config = config;

    /// <summary>
    ///    Implementación del método Login definido en la interfaz IAuthServices.
    ///   Actualmente, este método lanza una excepción NotImplementedException, 
    /// lo que indica que la lógica de autenticación aún no ha sido implementada.
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<LoginResponse> Login(LoginDto loginDto)
    {
        try
        {   
            // Búsqueda del usuario en la base de datos utilizando el correo electrónico proporcionado en el LoginDto.
            // Se incluye la entidad Role para obtener la información del rol del usuario durante la autenticación.
            // Si no se encuentra el usuario o la contraseña no coincide, se devuelve una respuesta indicando que las credenciales son incorrectas.
            var user = await _contextDb.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u=> u.Email == loginDto.Email);

            // Verificación de las credenciales del usuario. Si el usuario no existe o la contraseña no coincide, se devuelve una respuesta de error.
            if(user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                // Retorno de una respuesta indicando que el correo electrónico o la contraseña son incorrectos.
                return new LoginResponse
                {
                    Success = false,
                    Message = "Correo electrónico o contraseña incorrectos",
                    Data = null
                };
            }

            // Verificación del estado del usuario. Si el usuario está inactivo, se devuelve una respuesta indicando que el usuario está inactivo y que debe contactar al administrador.
            if (!user.Status)
            {
                // Retorno de una respuesta indicando que el usuario está inactivo y que debe contactar al administrador.
                return new LoginResponse
                {
                    Success = false,
                    Message = "Usuario inactivo, por favor contacte al administrador",
                    Data = null
                };
            }

            user.Name = "Francisco2 ";

            await _contextDb.SaveChangesAsync();
            // Si las credenciales son correctas y el usuario está activo, 
            // se genera un token JWT para el usuario utilizando la clase GenerateToken, 
            // y se devuelve una respuesta indicando que el inicio de sesión fue exitoso, 
            // junto con los datos relevantes del usuario autenticado.
            return new LoginResponse
            {
                Success = true,
                Message = "Login exitoso",
                Data = new DataResponseLogin
                {
                    Token = GenerateToken.CreateToken(user, _config),
                    UserId = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role.Name
                }
            };
        }
        catch (Exception ex)
        {
            return new LoginResponse
            {
                Success = false,
                Message = $"Error durante el login: {ex.Message}",
                Data = null
            };
        }
    }
        
    
}