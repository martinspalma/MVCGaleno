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
        public async Task<IActionResult> Create([Bind("IdAfiliado,NombreCompleto,Dni,tipoPlan,mail,telefono")] Afiliado afiliado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(afiliado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(afiliado);
        }

        // GET: Afiliado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afiliado = await _context.Afiliados.FindAsync(id);
            if (afiliado == null)
            {
                return NotFound();
            }
            return View(afiliado);
        }

        // POST: Afiliado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAfiliado,NombreCompleto,Dni,tipoPlan,mail,telefono")] Afiliado afiliado)
        {
            if (id != afiliado.IdAfiliado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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

            var afiliado = await _context.Afiliados
                .FirstOrDefaultAsync(m => m.IdAfiliado == id);
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
