using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProyectoDivine.Src.Model;

namespace ProyectoDivine.Src.Utils;
/// <summary>
/// Clase estática que proporciona un método para generar un token JWT (JSON Web Token) para un usuario autenticado.
/// El método CreateToken toma un objeto User y una instancia de IConfiguration para acceder a la configuración de la aplicación,
/// y devuelve un token JWT que contiene las reclamaciones del usuario, como su identificador, correo electrónico, nombre completo y rol.
/// </summary>
public static class GenerateToken
{
    /// <summary>
    /// Genera un token JWT para un usuario autenticado.
    /// El token incluye reclamaciones que representan la información del usuario, como su ID, correo electrónico, nombre completo y rol.
    /// El token se firma utilizando una clave simétrica definida en la configuración de la aplicación, y tiene una fecha de expiración establecida en 1 hora a partir del momento de su creación.
    /// </summary>
    /// <param name="user">El usuario para quien se genera el token.</param>
    /// <param name="config">La configuración de la aplicación.</param>
    /// <returns>El token JWT generado.</returns>
    public static string CreateToken(User user, IConfiguration config)
    {
        // Creación de la clave de seguridad simétrica utilizando la clave definida en la configuración de la aplicación.
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:key"]!) 
        );
        // Creación de las credenciales de firma utilizando la clave de seguridad y el algoritmo HMAC SHA256.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        // Definición de las reclamaciones que se incluirán en el token JWT, representando la información del usuario.
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
            new Claim(ClaimTypes.Role, user.Role.Name) 
        };
        // Creación del token JWT utilizando las reclamaciones, las credenciales de firma, y la configuración de emisor, audiencia y expiración.
        var token = new JwtSecurityToken(
            issuer: config["Jwt:issuer"],
            audience: config["Jwt:audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );
        // Retorno del token JWT generado como una cadena.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}