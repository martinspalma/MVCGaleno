using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCGaleno.Models
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCita { get; set; }

        [DisplayFormat(DataFormatString = "Fecha {0:dd/MM/yy} Hora {0:HH:mm}")]
        public DateTime fechaCita { get; set; }

        public Boolean estaDisponible { get; set; }

        [ForeignKey("PrestadorMedico")]
        public int IdPrestador { get; set; }

        public PrestadorMedico? PrestadorMedico { get; set; }

    }
}