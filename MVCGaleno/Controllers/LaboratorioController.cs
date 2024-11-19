using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCGaleno.Models;

namespace MVCGaleno.Controllers
{
    public class LaboratorioController : Controller
    {
        // GET: LaboratorioController
        public ActionResult Index()
        {
            return View();
        }

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


        // Acción POST para manejar la carga de archivos
        [HttpPost]
        public async Task<IActionResult> SubirEstudio(Laboratorio model)
        {
            if (ModelState.IsValid)
            {
                if (model.ArchivoEstudio != null && model.ArchivoEstudio.Length > 0)
                {
                    // Validar tipo de archivo
                    var fileExtension = Path.GetExtension(model.ArchivoEstudio.FileName).ToLower();
                    if (fileExtension != ".pdf")
                    {
                        ModelState.AddModelError("ArchivoEstudio", "Solo se permiten archivos PDF.");
                        return View("SubirEstudio", model); // Asegúrate de que el nombre de la vista es correcto
                    }

                    // Validar tamaño del archivo (por ejemplo, no mayor a 5MB)
                    if (model.ArchivoEstudio.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ArchivoEstudio", "El archivo no debe exceder los 5 MB.");
                        return View("SubirEstudio", model); // Asegúrate de que el nombre de la vista es correcto
                    }
                    // crear carpeta de destino 
                    var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }
                    // Guardar el archivo
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", model.ArchivoEstudio.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ArchivoEstudio.CopyToAsync(stream);
                    }

                    // Mensaje de éxito
                    ViewBag.Mensaje = "Estudio subido correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ArchivoEstudio", "El archivo de estudio es obligatorio.");
                }
            }

            // Si no es válido, devolver el formulario con los errores
            return View("SubirEstudio", model); // Asegúrate de que el nombre de la vista es correcto
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
        public IActionResult SubirEstudio()
        {
            return View(new Laboratorio());
        }
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