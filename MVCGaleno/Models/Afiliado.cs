using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MVCGaleno.Models;

namespace MVCGaleno.Models
{
    public class Afiliado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAfiliado { get; set; }
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string Dni { get; set; }

        [EnumDataType(typeof(TipoPlan))]
        public TipoPlan tipoPlan { get; set; }
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El formato del correo no es válido.")]
        public string mail { get; set; }

        public string NombreCompleto { get; set; }

        

        public String telefono { get; set; }
    }
}
