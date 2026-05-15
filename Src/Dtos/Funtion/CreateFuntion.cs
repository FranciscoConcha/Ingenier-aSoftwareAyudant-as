using System.ComponentModel.DataAnnotations;
using ProyectoDivine.Src.Validators;

namespace ProyectoDivine.Src.Dtos.Funtion;
public class CreateFuntion
{
    [Required(ErrorMessage = "El nombre de la función es obligatorio.")]
    public string Name {get;set;} = null!;
    [Required(ErrorMessage = "La descripción de la función es obligatoria.")]
    public string Description {get;set;} = null!;
    [Required(ErrorMessage = "La fecha de la función es obligatoria.")]
    public string DateFunction {get;set;} = null!;
    [Required(ErrorMessage = "La hora de la función es obligatoria.")]
    [CustomValidation(typeof(CreateFuntion), nameof(ValidatorDtos.ValidateDateTime))]
    public string TimeFunction {get;set;} = null!;
    [Required(ErrorMessage ="La imagen de la función es obligatoria.")]
    public IFormFile? Image {get;set;} 
}

public class CreateFuntionData
{
    public int Id {get;set;}
    public string Name {get;set;} = null!;
    public string Description {get;set;} = null!;
    public string DateFunction {get;set;} = null!;
    public string TimeFunction {get;set;} = null!;
    public bool State {get;set;}
    public string ImageUrl {get;set;} = null!;
}

public class CreateFuntionResponse
{
    public string Message {get;set;} = null!;
    public bool Success {get;set;}
    public CreateFuntionData Data {get;set;} = null!;
}