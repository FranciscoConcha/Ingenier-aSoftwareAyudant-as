namespace ProyectoDivine.Src.Dtos.Users;
/// <summary>
/// Clase que representa el Data Transfer Object (DTO) para el proceso de inicio de sesión de un usuario.
/// Contiene propiedades para el correo electrónico y la contraseña del usuario, que son necesarias para autenticar 
/// al usuario en el sistema. 
/// Este DTO se utiliza para transferir los datos de inicio de sesión desde el cliente al servidor de manera estructurada y segura.
/// </summary>
public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
/// <summary>
/// Clase que representa la estructura de la respuesta que se devuelve al cliente después de un intento de inicio de sesión.
/// Contiene propiedades para indicar si el inicio de sesión fue exitoso, un mensaje descriptivo sobre el resultado del intento de inicio de sesión, 
/// y un objeto DataResponseLogin que contiene los datos relevantes del usuario autenticado, 
/// como el token de autenticación, el ID del usuario, su nombre, apellido, correo electrónico y rol.
/// </summary>
public class DataResponseLogin
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
/// <summary>
/// Clase que representa la estructura de la respuesta general para el proceso de inicio de sesión.
/// Contiene una propiedad Success que indica si el inicio de sesión fue exitoso o no,
/// una propiedad Message que proporciona información adicional sobre el resultado del intento de inicio de sesión,
/// y una propiedad Data que contiene un objeto DataResponseLogin con los datos relevantes del usuario autenticado 
/// en caso de que el inicio de sesión haya sido exitoso.
/// </summary>
public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public DataResponseLogin? Data { get; set; }
}