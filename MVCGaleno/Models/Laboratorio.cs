using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MVCGaleno.Models
{
    public class Laboratorio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLaboratorio { get; set; }

        [ForeignKey("PrestadorMedico")]
        public int IdPrestador { get; set; }

        [ForeignKey("Afiliado")]
        public int IdAfiliado { get; set; }

        [Required]
        [Display(Name = "Archivo de Estudio")]
        public string RutaArchivo { get; set; } 

        [NotMapped]
        public IFormFile ArchivoEstudio { get; set; }
    }
    
}