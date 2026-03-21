//Nos permite usar las clases y métodos definidos en el espacio de nombres ProyectoDivine.Src.Model
namespace ProyectoDivine.Src.Model;
/// <summary>
/// Clase para representar un usuario dentro del sistema. 
/// Contiene propiedades como Id, Name, LastName, Email, Rut, Phone, Password y Status. 
/// Además, tiene una relación con la clase Role a través de la propiedad RoleId.
/// </summary>
public class User
{
    public int Id{get;set;}
    public string Name{get;set;} = string.Empty;
    public string LastName{get;set;} = string.Empty;
    public string Email{get;set;} = string.Empty;
    public string Rut {get;set;} = string.Empty;
    public string Phone {get;set;} = string.Empty;
    public string Password {get;set;} = string.Empty;
    public bool Status {get;set;}
    
    public Role Role { get; set; } = null!;
    public int RoleId { get; set; }

}

