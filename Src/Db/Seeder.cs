using BCrypt.Net;
using ProyectoDivine.Src.Model;

namespace ProyectoDivine.Src.Db;
/// <summary>
/// Clase Seeder para poblar la base de datos con datos iniciales.
/// Contiene un método Seed que verifica si existen roles y usuarios en la base de datos,
/// y si no existen, crea un rol de administrador y un rol de usuario, así como un usuario administrador y un usuario normal con contraseñas hasheadas utilizando BCrypt.
/// </summary>
/// <param name="context"></param>
public class Seeder(ContextDb context)
{
    /// <summary>
    /// Instancia del contexto de la base de datos que se 
    /// utiliza para interactuar con la base de datos y realizar operaciones de inserción de datos.
    /// </summary>
    private readonly ContextDb _context = context;
    /// <summary>
    /// Método para poblar la base de datos con datos iniciales. 
    /// Verifica si existen roles y usuarios, y si no existen, los crea con contraseñas hasheadas utilizando BCrypt.
    /// El método Seed se encarga de asegurar que la base de datos tenga al menos un rol de administrador,
    ///  un rol de usuario, un usuario administrador y un usuario normal, 
    /// lo que permite que la aplicación tenga datos iniciales para funcionar correctamente.
    /// </summary>
    public async Task Seed()
    {
        // Verificación de la existencia de roles en la base de datos. 
        // Si no existen, se crean los roles de administrador y usuario.
        if (!_context.Roles.Any())
        {
            var adminRole = new Role { Name = "Admin" };
            var userRole = new Role { Name = "User" };

            await _context.Roles.AddRangeAsync(adminRole, userRole);
            await _context.SaveChangesAsync();
        }
        // Verificación de la existencia de usuarios en la base de datos.
        // Si no existen, se crean un usuario administrador y un usuario normal 
        // con contraseñas hasheadas utilizando BCrypt.
        if (!_context.Users.Any())
        {
            var adminUser = new User
            {
                Name = "Admin",
                LastName = "User",
                Email = "admin@divine.cl",
                Rut = "12345678-9",
                Phone = "123456789",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123"), 
                Status = true,
                RoleId = _context.Roles.First(r => r.Name == "Admin").Id
            };  
            var normalUser = new User
            {
                Name = "Normal",
                LastName = "User",
                Email = "user@divine.cl",
                Rut = "98765432-1",
                Phone = "987654321",
                Password = BCrypt.Net.BCrypt.HashPassword("user123"), // Hasheamos la contraseña para el usuario normal
                Status = true,
                RoleId = _context.Roles.First(r => r.Name == "User").Id
            };

            await _context.Users.AddRangeAsync(adminUser, normalUser);
            await _context.SaveChangesAsync();
        }
    }
}