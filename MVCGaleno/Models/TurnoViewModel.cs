using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVCGaleno.Models
{
    public class TurnoViewModel
    {
        [Required]
        public int IdCita { get; set; }
        [Required]
        public int IdAfiliado { get; set; }
        [Required]
        public string NombreAfiliado { get; set; }
        [Required]
        public int IdPrestador { get; set; }
        [Required]
        public string NombrePrestador { get; set; }
        [Required]
        public Especialidad Especialidad { get; set; }
        public String FechaCita { get; set; }
    }
}