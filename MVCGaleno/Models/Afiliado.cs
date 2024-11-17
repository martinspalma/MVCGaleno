using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCGaleno.Models
{
    public class Afiliado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAfiliado { get; set; }
        public string NombreCompleto { get; set; }
        public string Dni { get; set; }

        [EnumDataType(typeof(TipoPlan))]
        public TipoPlan tipoPlan { get; set; }
        public string mail { get; set; }
        public int telefono { get; set; }

    }
}
