using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCGaleno.Models
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCita { get; set; }

        [Display(Name = "Fecha cita")]
        public DateTime fechaCita { get; set; }

        public Boolean estaDisponible { get; set; }

        public int IdPrestador { get; set; }

    }
}

