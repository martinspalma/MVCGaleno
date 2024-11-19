using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MVCGaleno.Models;

namespace MVCGaleno.Models
{
    public class PrestadorMedico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPrestador { get; set; }
        [EnumDataType(typeof(Especialidad))]
        public Especialidad Especialidad { get; set; }
        public string NombreCompleto { get; set; }
        public string MatriculaProfesional { get; set; }
        public string MailMedico { get; set; }
        public int TelefonoMedico { get; set; }
        public string DireccionMedico { get; set; }
    }
}


  