using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MVCGaleno.Models;

namespace MVCGaleno.Models
{
    public class Turno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTurno { get; set; }

        [Required]
        [EnumDataType(typeof(Especialidad))]
        public Especialidad Especialidad { get; set; }

        [Required]
        public PrestadorMedico PrestadorMedico { get; set; }

        [Required]
        public Afiliado Afiliado { get; set; }
        [DisplayFormat(DataFormatString = "Fecha {0:dd/MM/yy} Hora {0:HH:mm}")]
        public DateTime? fechaCita { get; set; }
    }
}