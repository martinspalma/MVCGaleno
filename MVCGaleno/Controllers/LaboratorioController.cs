using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGaleno.Context;
using MVCGaleno.Models;

namespace MVCGaleno.Controllers
{
    public class LaboratorioController : Controller
    {
        private readonly GalenoDatabaseContext _context;
        public LaboratorioController(GalenoDatabaseContext context)
        {
            _context = context;
        }

        //mod
        [HttpGet]
        public IActionResult SubirEstudio()
        {
            // Obtener listado de médicos
            var prestadores = _context.Medicos
                .Select(m => new SelectListItem
                {
                    Value = m.IdPrestador.ToString(),
                    Text = m.NombreCompleto
                })
                .ToList();

            // Enviar la lista de médicos a la vista a través de ViewBag
            ViewBag.PrestadorList = prestadores;

            // Devolver el modelo vacío a la vista
            return View(new Laboratorio());
        }

        [HttpPost]
        public async Task<IActionResult> SubirEstudio(Laboratorio model, string dniAfiliado)
        {
            // Recargar la lista de médicos para el DropDownList en caso de error
            var prestadores = _context.Medicos
                .Select(m => new SelectListItem
                {
                    Value = m.IdPrestador.ToString(),
                    Text = m.NombreCompleto
                })
                .ToList();
            ViewBag.PrestadorList = prestadores;

            // Validar el modelo
           // if (!ModelState.IsValid)
            //    return View(model);

            // Validar existencia del afiliado en la base de datos
            var afiliado = await _context.Afiliados.FirstOrDefaultAsync(a => a.Dni == dniAfiliado);
            if (afiliado == null)
            {
                ViewData["DniAfiliadoError"] = "El DNI ingresado no existe en la base de datos.";
                return View(model);
            }

            // Verificar que ArchivoEstudio no es null y tiene un archivo cargado
            if (model.ArchivoEstudio != null)
            {
                Debug.WriteLine($"ArchivoEstudio no es null. Nombre del archivo: {model.ArchivoEstudio.FileName}");
            }
            else
            {
                Debug.WriteLine("ArchivoEstudio es null. No se ha cargado ningún archivo.");
            }

            // Validar y guardar el archivo
            if (model.ArchivoEstudio != null && model.ArchivoEstudio.Length > 0)
            {
                // Validar tamaño del archivo (máximo 5MB)
                if (model.ArchivoEstudio.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("ArchivoEstudio", "El archivo no debe exceder los 5 MB.");
                    return View(model);
                }

                // Definir la ruta absoluta de almacenamiento
                var uploadDirectory = Path.Combine(@"C:\Users\marti\OneDrive\Desktop\uploads");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory); // Crear la carpeta si no existe
                }

                var filePath = Path.Combine(uploadDirectory, model.ArchivoEstudio.FileName);
                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ArchivoEstudio.CopyToAsync(stream); // Guardar el archivo
                    }

                    // Establecer la ruta del archivo en el modelo para guardarlo en la base de datos
                    model.RutaArchivo = filePath;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al guardar el archivo: {ex.Message}");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("ArchivoEstudio", "El archivo de estudio es obligatorio.");
                return View(model);
            }

            // Guardar los datos en la base de datos
            try
            {
                var laboratorio = new Laboratorio
                {
                    IdPrestador = model.IdPrestador,
                    IdAfiliado = afiliado.IdAfiliado,
                    RutaArchivo = model.RutaArchivo // Guardar la ruta absoluta
                };

                _context.Laboratorio.Add(laboratorio);
                await _context.SaveChangesAsync();

                // Redirigir con mensaje de éxito
                TempData["SuccessMessage"] = "Estudio subido correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al guardar en la base de datos: {ex.Message}");
                return View(model);
            }
        }


        public async Task<IActionResult> Index(int? medicoId)
        {
            // Si se pasa un medicoId, filtramos por médico
            var documentos = _context.Laboratorio.AsQueryable();

            if (medicoId.HasValue)
            {
                documentos = documentos.Where(l => l.IdPrestador == medicoId);
            }

            // Recuperar la lista de documentos
            var listaDocumentos = await documentos
                .ToListAsync(); // Recuperar la lista sin incluir detalles del afiliado o médico todavía

            // Crear una lista de documentos con la información del médico y del afiliado agregada manualmente
            var listaDocumentosConMedicoYAfiliado = listaDocumentos.Select(l => new DocumentoViewModel
            {
                IdLaboratorio = l.IdLaboratorio,
                RutaArchivo = l.RutaArchivo,
                // Obtener el nombre del médico
                MedicoNombre = _context.Medicos
                                        .Where(m => m.IdPrestador == l.IdPrestador)
                                        .Select(m => m.NombreCompleto)
                                        .FirstOrDefault(),
                // Obtener el nombre del afiliado
                AfiliadoNombre = _context.Afiliados
                                          .Where(a => a.IdAfiliado == l.IdAfiliado)
                                          .Select(a => a.NombreCompleto)
                                          .FirstOrDefault()
            }).ToList();

            // Pasamos la lista de documentos con la información del médico y del afiliado a la vista
            return View(listaDocumentosConMedicoYAfiliado);
        }


        // Otros métodos del controlador...







        // GET: LaboratorioController
        /*
        public ActionResult Index()
        {
            return View();
        }
        */
        // GET: LaboratorioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LaboratorioController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: LaboratorioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LaboratorioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LaboratorioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LaboratorioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
       /* public IActionResult SubirEstudio()
        {
            return View(new Laboratorio());
        }
       */
        [HttpPost]


        // POST: LaboratorioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}