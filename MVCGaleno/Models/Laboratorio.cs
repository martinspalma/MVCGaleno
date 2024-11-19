using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MVCGaleno.Models
{
    public class Laboratorio
    {
        public int IdLaboratorio { get; set; }

        [Required]
        [Display(Name = "Especialidad")]
        public Especialidad Especialidad { get; set; }

        [Required]
        [Display(Name = "Prestador Médico")]
        public string PrestadorMedico { get; set; }

        [Required]
        [Display(Name = "Archivo de Estudio")]
        public IFormFile ArchivoEstudio { get; set; }
    }
}