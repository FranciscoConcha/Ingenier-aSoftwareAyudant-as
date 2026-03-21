namespace ProyectoDivine.Src.Model;
/// <summary>
/// Clase para representar un rol dentro del sistema. 
/// Contiene propiedades como Id y Name.
/// Además, tiene una relación con la clase User a través de la propiedad User, 
/// que es una colección de usuarios asociados a ese rol.
/// </summary>
public class Role()
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = [];
}