using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGaleno.Context;
using MVCGaleno.Models;

namespace MVCGaleno.Controllers
{
    public class PrestadorMedicoController : Controller
    {
        private readonly GalenoDatabaseContext _context;

        public PrestadorMedicoController(GalenoDatabaseContext context)
        {
            _context = context;
        }

        // GET: PrestadorMedicoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicos.ToListAsync());
        }

        // GET: PrestadorMedicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestadorMedico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.IdPrestador == id);
            if (prestadorMedico == null)
            {
                return NotFound();
            }

            return View(prestadorMedico);
        }

        // GET: PrestadorMedicoes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrestadorMedicoCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var DireccionMedico = $"{model.Calle}: {model.NumeroCalle}, Piso {model.Piso}, Depto {model.Depto}, Loc: {model.Localidad}";
                var NombreCompleto = $"Dr./Dra. {model.Apellido}, {model.Nombre}";
                var telefonoCompleto = $"({model.CodigoArea}) {model.Caracteristica} - {model.Numero}";
                var prestadorMedico = new PrestadorMedico
                {
                    Especialidad= model.Especialidad,
                    NombreCompleto = NombreCompleto,
                    MatriculaProfesional = model.MatriculaProfesional,
                    MailMedico = model.MailMedico,
                    DireccionMedico = DireccionMedico,
                    TelefonoMedico = telefonoCompleto 
                };

                _context.Medicos.Add(prestadorMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: PrestadorMedicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*COMENTADO ORIGINAL
          [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("IdPrestador,Especialidad,NombreCompleto,MatriculaProfesional,MailMedico,TelefonoMedico,DireccionMedico")] PrestadorMedico prestadorMedico)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(prestadorMedico);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(prestadorMedico);
         }
        ES HASTA ACA LA JODA
        */
        // GET: PrestadorMedicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }
            var prestadorMedico = await _context.Medicos.FindAsync(id);
            
            String finCalle = ": ";
            String finNumeroCalle = ", Piso";
            String finPiso = ", Depto";
            String finDepto = ", Loc:";
            int inicioNumeroCalle = prestadorMedico.DireccionMedico.IndexOf(finCalle,0);
            int inicioPiso = prestadorMedico.DireccionMedico.IndexOf(finNumeroCalle, inicioNumeroCalle);
            int inicioDpto = prestadorMedico.DireccionMedico.IndexOf(finPiso, inicioNumeroCalle);
            int inicioLoc = prestadorMedico.DireccionMedico.IndexOf(finDepto, inicioNumeroCalle);
            int inicioLoca = inicioLoc + finDepto.Length;
            
            char inicioApellido = ' ';
            char finApellido = ',';
            int posicionInicioApellido = prestadorMedico.NombreCompleto.IndexOf(inicioApellido, 0);
            int posicionFinApellido = prestadorMedico.NombreCompleto.IndexOf(finApellido, 0);
            int tamanioApellido = posicionFinApellido - posicionInicioApellido;
            int inicioNombre = (1 + posicionFinApellido);

            var nuevo = new PrestadorMedicoCreateViewModel
            
            {

                //= $"{model.Calle}: {model.NumeroCalle}, Piso {model.Piso}, Depto {model.Depto}, Loc: {model.Localidad}";
                IdPrestador= prestadorMedico.IdPrestador,       
                Especialidad = prestadorMedico.Especialidad,
                CodigoArea= prestadorMedico.TelefonoMedico.Substring(1,3),
                Caracteristica= prestadorMedico.TelefonoMedico.Substring(6, 4),
                Numero= prestadorMedico.TelefonoMedico.Substring(13, 4),
                
                MailMedico=prestadorMedico.MailMedico,
                Apellido = prestadorMedico.NombreCompleto.Substring(posicionInicioApellido, tamanioApellido),
                Nombre =prestadorMedico.NombreCompleto.Substring(inicioNombre),

                MatriculaProfesional =prestadorMedico.MatriculaProfesional,
                Calle = prestadorMedico.DireccionMedico.Substring(0, inicioNumeroCalle),
                NumeroCalle = prestadorMedico.DireccionMedico.Substring((inicioNumeroCalle+ finCalle.Length), 4),
                Piso = prestadorMedico.DireccionMedico.Substring(inicioPiso+ finNumeroCalle.Length, 2),
                Depto = prestadorMedico.DireccionMedico.Substring(inicioDpto+ finPiso.Length, 2),
                Localidad=prestadorMedico.DireccionMedico.Substring(inicioLoca),
            }
            ;
             if (nuevo == null)
             {
                 return NotFound();
             }
             return View(nuevo); // aca tendria que enviar ViewModel
         }
        
        // POST: PrestadorMedicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PrestadorMedicoCreateViewModel model)
        {
            var prestadorMedico = await _context.Medicos.FindAsync(id);
            if (id != model.IdPrestador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var telefonoCompleto = $"({model.CodigoArea}) {model.Caracteristica} - {model.Numero}";
                var NombreCompleto = $"Dr./Dra. {model.Apellido}, {model.Nombre}";
                var DireccionMedico = $"{model.Calle}: {model.NumeroCalle}, Piso {model.Piso}, Depto {model.Depto}, Loc: {model.Localidad}";

                prestadorMedico.Especialidad = model.Especialidad;
                    prestadorMedico.NombreCompleto = NombreCompleto;
                    prestadorMedico.MatriculaProfesional = model.MatriculaProfesional;
                    prestadorMedico.MailMedico = model.MailMedico;
                    prestadorMedico.DireccionMedico = DireccionMedico;
                    prestadorMedico.TelefonoMedico = telefonoCompleto; // Guardamos el formato requerido
                
                try
                {
                    _context.Update(prestadorMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestadorMedicoExists(prestadorMedico.IdPrestador))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(prestadorMedico);
        }

        // GET: PrestadorMedicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestadorMedico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.IdPrestador == id);
            if (prestadorMedico == null)
            {
                return NotFound();
            }

            return View(prestadorMedico);
        }

        // POST: PrestadorMedicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestadorMedico = await _context.Medicos.FindAsync(id);
            if (prestadorMedico != null)
            {
                _context.Medicos.Remove(prestadorMedico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestadorMedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.IdPrestador == id);
        }
    }
}