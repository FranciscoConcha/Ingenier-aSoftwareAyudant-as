namespace ProyectoDivine.Src.Model;
/// <summary>
/// Clase que representa una función en el contexto de la aplicación.
/// Contiene propiedades como Id, Name, Description, DateFunction, TimeFunction e Image,
/// </summary>
public class Funtion
{
    public int Id {get;set;}
    public string Name {get;set;} = null!;
    public string Description {get;set;} = null!;
    public string DateFunction {get;set;} = null!;
    public string TimeFunction {get;set;} = null!;
    public bool State {get;set;}
    public string Image {get;set;} = null!;
}