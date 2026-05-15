using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace ProyectoDivine.Src.Validators;
public class ValidatorDtos
{
    public static ValidationResult? ValidateDateTime(string dateFuntion)
    {
        // Validar que la fecha de la función sea mayor a la fecha actual
        // Puedes ajustar el formato de fecha según tus necesidades
        if (DateTime.TryParse(dateFuntion, out var date))
        {
            // Comparar solo las fechas sin considerar la hora
            if (date.Date > DateTime.Now.Date)
            {
                return ValidationResult.Success;
            }
        }
        return new ValidationResult("La fecha de la función debe ser mayor a la fecha actual.");

    }
}