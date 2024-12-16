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
    public class CitasController : Controller
    {
        private readonly GalenoDatabaseContext _context;

        public CitasController(GalenoDatabaseContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var galenosDatabaseContext = _context.Citas.Include(c => c.PrestadorMedico);
            return View(await galenosDatabaseContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.PrestadorMedico)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            // Cargar los prestadores médicos
            var prestadores = _context.Medicos
                .Select(p => new SelectListItem
                {
                    Value = p.IdPrestador.ToString(),
                    Text = p.NombreCompleto
                })
                .ToList();

            ViewBag.IdPrestador = prestadores;

            return View();
        }


        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,fechaCita,estaDisponible,IdPrestador")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                // Acá se verifica que no exista otra cita con los mismos datos
                var citaDuplicada = await _context.Citas.AnyAsync(c => c.fechaCita == cita.fechaCita && c.IdPrestador == cita.IdPrestador);

                if (citaDuplicada)
                {
                    // En caso de que exista:
                    ModelState.AddModelError("", "La cita ya existe");

                    ViewData["IdPrestador"] = new SelectList(
                         _context.Medicos,
                          "IdPrestador",
                          "NombreCompleto",
                          cita.IdPrestador
                    );
                    return View(cita); 
                }

                // Si no existe, se crea una nueva cita
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPrestador"] = GetListaDePrestadores(cita.IdPrestador);

            return View(cita); 
        }


        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
           
            ViewData["IdPrestador"] = GetListaDePrestadores(cita.IdPrestador);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,fechaCita,estaDisponible,IdPrestador")] Cita cita)
        {
            if (id != cita.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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
            ViewData["IdPrestador"] = GetListaDePrestadores(cita.IdPrestador);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.PrestadorMedico)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }

        private SelectList GetListaDePrestadores(int? selectedId = null)
        {
            return new SelectList(_context.Medicos, "IdPrestador", "NombreCompleto", selectedId);
        }


    }
}
