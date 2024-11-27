using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCGaleno.Models
{
    public class CreateViewModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAfiliado { get; set; }
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string Apellido { get; set; }
        public string Dni { get; set; }
        

        [EnumDataType(typeof(TipoPlan))]
        public TipoPlan tipoPlan { get; set; }
        public string mail { get; set; }
        [DisplayFormat(DataFormatString = " {0:(0##)-####-####}")]
        public string CodigoArea { get; set; }

        [Required(ErrorMessage = "La caracteristica es obligatorio.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El número de teléfono debe tener el formato XXXX.")]
        public string Caracteristica { get; set; }
        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El número de teléfono debe tener el formato XXXX.")]
        
        public string Numero { get; set; }

    }
}
