namespace MVCGaleno.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FechaMayorIgualHoyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime fechaCita)
            {
                if (fechaCita.Date < DateTime.Today)
                {
                    return new ValidationResult("La fecha de la cita no puede ser anterior a hoy.");
                }
            }
            return ValidationResult.Success;
        }
    }

}
