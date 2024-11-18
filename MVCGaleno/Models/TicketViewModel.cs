namespace MVCGaleno.Models
{
    public class TicketViewModel
    {
        public string NombreCompletoAfiliado { get; set; }
        public string DniAfiliado { get; set; }
        public string NombreCompletoPrestadorMedico { get; set; }
        public string MatriculaPrestadorMedico { get; set; }
        public string MailPrestadorMedico { get; set; }
        public string TelefonoPrestadorMedico { get; set; }
        public string DireccionPrestadorMedico { get; set; }
        public DateTime FechaCita { get; set; }
    }
}