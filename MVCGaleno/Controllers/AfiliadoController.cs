using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGaleno.Context;
using MVCGaleno.Models;


namespace MVCGalenos.Controllers
{
    public class AfiliadoController : Controller
    {
        private readonly GalenoDatabaseContext _context;

        public AfiliadoController(GalenoDatabaseContext context)
        {
            _context = context;
        }

        // GET: Afiliado
        public async Task<IActionResult> Index()
        {
            return View(await _context.Afiliados.ToListAsync());
        }

        // GET: Afiliado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afiliado = await _context.Afiliados
                .FirstOrDefaultAsync(m => m.IdAfiliado == id);
            if (afiliado == null)
            {
                return NotFound();
            }

            return View(afiliado);
        }

        // GET: Afiliado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Afiliado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var telefonoCompleto = $"({model.CodigoArea}) {model.Caracteristica} - {model.Numero}";
                var NombreCompleto = $"{model.Nombre} {model.Apellido}";
                var afiliado = new Afiliado
                {
                    Dni = model.Dni,
                    mail = model.mail,
                    tipoPlan = model.tipoPlan,
                    telefono = telefonoCompleto,
                    NombreCompleto = NombreCompleto,

                };

                _context.Afiliados.Add(afiliado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Afiliado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var afiliado = await _context.Afiliados.FindAsync(id);

            char finNombre = ' ';
            int posicionFinNombre = afiliado.NombreCompleto.IndexOf(finNombre, 2);
            int inicioApellido = ((afiliado.NombreCompleto.Length) - posicionFinNombre);
            var nuevo = new CreateViewModel
            {
                IdAfiliado=afiliado.IdAfiliado,
                Nombre = afiliado.NombreCompleto.Substring(0, posicionFinNombre),
                Apellido = afiliado.NombreCompleto.Substring(inicioApellido),
                Dni = afiliado.Dni,
                tipoPlan = afiliado.tipoPlan,
                mail = afiliado.mail,
                CodigoArea = afiliado.telefono.Substring(1, 3),
                Caracteristica = afiliado.telefono.Substring(6, 4),
                Numero = afiliado.telefono.Substring(13, 4),
            };

            if (nuevo == null)
            {
                return NotFound();
            }
            return View(nuevo);
        }

        // POST: Afiliado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateViewModel model )
        {
            var afiliado= await _context.Afiliados.FindAsync(id);
            if (id != model.IdAfiliado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var telefonoCompleto = $"({model.CodigoArea}) {model.Caracteristica} - {model.Numero}";
                var NombreCompleto = $" {model.Nombre} {model.Apellido}";

                    afiliado.tipoPlan = model.tipoPlan;
                    afiliado.Dni = model.Dni;
                    afiliado.mail = model.mail;
                    afiliado.NombreCompleto = NombreCompleto;
                    afiliado.telefono = telefonoCompleto;
                try
                {
                    _context.Update(afiliado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfiliadoExists(afiliado.IdAfiliado))
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
            return View(afiliado);
        }

        // GET: Afiliado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afiliado = await _context.Afiliados.FirstOrDefaultAsync(m => m.IdAfiliado == id);
            if (afiliado == null)
            {
                return NotFound();
            }

            return View(afiliado);
        }

        // POST: Afiliado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var afiliado = await _context.Afiliados.FindAsync(id);
            if (afiliado != null)
            {
                _context.Afiliados.Remove(afiliado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AfiliadoExists(int id)
        {
            return _context.Afiliados.Any(e => e.IdAfiliado == id);
        }
    }

}
