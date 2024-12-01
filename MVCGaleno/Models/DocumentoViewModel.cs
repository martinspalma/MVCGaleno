using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCGaleno.Models
{
    public class DocumentoViewModel
    {
        public int IdLaboratorio { get; set; }
        public string RutaArchivo { get; set; }
        public string MedicoNombre { get; set; }
        public string AfiliadoNombre { get; set; }
    }
}
