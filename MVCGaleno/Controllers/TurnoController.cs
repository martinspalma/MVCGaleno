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
    public class TurnoController : Controller
    {
        private readonly GalenoDatabaseContext _context;

        public TurnoController(GalenoDatabaseContext context)
        {
            _context = context;
        }

        // GET: Turno/SelectSpecialty 
        public IActionResult SelectSpecialty()
        {
            var specialties = Enum.GetValues(typeof(Especialidad)).Cast<Especialidad>();
            return View(specialties);
        }
        // POST: Turno/SelectSpecialty
        [HttpPost]
        public IActionResult SelectSpecialty(Especialidad especialidad)
        {
            return RedirectToAction("SelectMedico", new { especialidad });
        }

        // GET: Turno/SelectMedico
        public IActionResult SelectMedico(Especialidad especialidad)
        {
            var medicos = _context.Medicos.Where(m => m.Especialidad == especialidad).ToList();
            return View(medicos);
        }

        // POST: Turno/SelectMedico
        [HttpPost]
        public IActionResult SelectMedico(int idPrestador)
        {
            return RedirectToAction("SelectCita", new { idPrestador });
        }
        // GET: Turno/SelectCita
        public IActionResult SelectCita(int idPrestador)
        {
            var citas = _context.Citas
                .Where(c => c.IdPrestador == idPrestador && c.estaDisponible)
                .ToList();
            return View(citas);
        }

        // POST: Turno/SelectCita
        [HttpPost]
        public IActionResult SelectCita2(int idCita)
        {
            return RedirectToAction("ConfirmTurno", new { idCita });
        }


        // GET: Turno/ConfirmTurno
        public IActionResult ConfirmTurno(int idCita)
        {
            var model = new ConfirmTurnoViewModel { IdCita = idCita };
            return View(model);
        }

        // POST: Turno/ConfirmTurno
        [HttpPost]
        public IActionResult ConfirmTurno(ConfirmTurnoViewModel model)
        {
            var afiliado = _context.Afiliados.FirstOrDefault(a => a.Dni.Trim() == model.Dni.Trim());
            if (afiliado == null)
            {
                ModelState.AddModelError("", "Afiliado no encontrado: ingrese correctamente el DNI.");
                return View(model);
            }

            var cita = _context.Citas.Find(model.IdCita);
            if (cita == null)
            {
                ModelState.AddModelError("", "Cita no disponible.");
                return View(model);
            }
            
            var prestadorMedico = _context.Medicos.FirstOrDefault(m => m.IdPrestador == cita.IdPrestador);
            if (prestadorMedico == null)
            {
                ModelState.AddModelError("", "Prestador médico no encontrado.");
                return View(model);
            }


            var turnoViewModel = new TurnoViewModel
            {
                IdCita = model.IdCita,
                IdAfiliado = afiliado.IdAfiliado,
                IdPrestador = prestadorMedico.IdPrestador,
                Especialidad = prestadorMedico.Especialidad,
                FechaCita = cita.fechaCita.ToString("dd/MM/yy HH:mm")
                

            };
            return RedirectToAction("Create", turnoViewModel);



        }

        // GET: Turno/Create
        public IActionResult Create(TurnoViewModel turnoViewModel)
        {
            return View(turnoViewModel);
        }

        // POST: Turno/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2(TurnoViewModel turnoViewModel)
        {
            if (ModelState.IsValid)
            {
                var cita = _context.Citas.FirstOrDefault(m => m.IdCita == turnoViewModel.IdCita);
                cita.estaDisponible = false;
                var turno = new Turno
                {
                    fechaCita = DateTime.Parse(turnoViewModel.FechaCita),
                    PrestadorMedico = _context.Medicos.FirstOrDefault(m => m.IdPrestador == turnoViewModel.IdPrestador),
                    Afiliado = _context.Afiliados.FirstOrDefault(a => a.IdAfiliado == turnoViewModel.IdAfiliado),
                    Especialidad = turnoViewModel.Especialidad
                };


                _context.Add(turno);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            return View(turnoViewModel);
        }


        // GET: Turnoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // .Include(t => t.PrestadorMedico) ESTA INSTRUCCION INCLUYE LOS ATRIBUTOS DE LA CLASE
            var turno = await _context.Turnos
                .Include(t => t.PrestadorMedico)
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }



        // GET: Turnoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            return View(turno);
        }
        // GET: Turno/Index
        public IActionResult Index()
        {
            var turnos = _context.Turnos.Include(t => t.PrestadorMedico).Include(t => t.Afiliado).ToList();
            return View(turnos);
        }

        // POST: Turnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTurno,Especialidad,fechaCita")] Turno turno)
        {
            if (id != turno.IdTurno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.IdTurno))
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
            return View(turno);
        }

        // GET: Turnoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.IdTurno == id);
        }
    }
}