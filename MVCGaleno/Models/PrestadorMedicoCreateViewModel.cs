using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MVCGaleno.Models
{
    public class PrestadorMedicoCreateViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPrestador { get; set; }
        [EnumDataType(typeof(Especialidad))]
        public Especialidad Especialidad { get; set; }

        [Required(ErrorMessage = "El código de área es obligatorio.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "El código de área debe tener 3 dígitos.")]
        public string CodigoArea { get; set; }

        [Required(ErrorMessage = "La caracteristica es obligatorio.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El número de teléfono debe tener el formato XXXX.")]
        public string Caracteristica { get; set; }
        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El número de teléfono debe tener el formato XXXX.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        public string MatriculaProfesional { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El formato del correo no es válido.")]
        public string MailMedico { get; set; }

        public string Calle { get; set; }
        public string NumeroCalle { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Localidad { get; set; }
        




    }
}
