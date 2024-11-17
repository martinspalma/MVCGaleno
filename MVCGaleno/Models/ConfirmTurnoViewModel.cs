using System.ComponentModel.DataAnnotations;

namespace MVCGaleno.Models
{
    public class ConfirmTurnoViewModel
    {
        [Required]
        public int IdCita { get; set; }
        [Required]
        [StringLength(50)]
        public string Dni { get; set; }
    }
}
