using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JuFlores.Data;
using JuFlores.Models;

namespace JuFlores.Controllers
{
    public class PecasFotosController : Controller
    {
        private readonly JuFloresDB _context;

        public PecasFotosController(JuFloresDB context)
        {
            _context = context;
        }

        // GET: PecasFotos
        public async Task<IActionResult> Index()
        {
            var juFloresDB = _context.PecasFotos.Include(p => p.Fotografia).Include(p => p.Peca);
            return View(await juFloresDB.ToListAsync());
        }

        // GET: PecasFotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecasFotos = await _context.PecasFotos
                .Include(p => p.Fotografia)
                .Include(p => p.Peca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pecasFotos == null)
            {
                return NotFound();
            }

            return View(pecasFotos);
        }

        // GET: PecasFotos/Create
        public IActionResult Create()
        {
            ViewData["FotografiaFK"] = new SelectList(_context.Fotografias, "Id", "Fotografia");
            ViewData["PecaFk"] = new SelectList(_context.Pecas, "Id", "Nome");
            return View();
        }

        // POST: PecasFotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PecaFk,Data,FotografiaFK")] PecasFotos pecasFotos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pecasFotos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FotografiaFK"] = new SelectList(_context.Fotografias, "Id", "Fotografia", pecasFotos.FotografiaFK);
            ViewData["PecaFk"] = new SelectList(_context.Pecas, "Id", "Nome", pecasFotos.PecaFk);
            return View(pecasFotos);
        }

        // GET: PecasFotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecasFotos = await _context.PecasFotos.FindAsync(id);
            if (pecasFotos == null)
            {
                return NotFound();
            }
            ViewData["FotografiaFK"] = new SelectList(_context.Fotografias, "Id", "Fotografia", pecasFotos.FotografiaFK);
            ViewData["PecaFk"] = new SelectList(_context.Pecas, "Id", "Nome", pecasFotos.PecaFk);
            return View(pecasFotos);
        }

        // POST: PecasFotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PecaFk,Data,FotografiaFK")] PecasFotos pecasFotos)
        {
            if (id != pecasFotos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pecasFotos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PecasFotosExists(pecasFotos.Id))
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
            ViewData["FotografiaFK"] = new SelectList(_context.Fotografias, "Id", "Fotografia", pecasFotos.FotografiaFK);
            ViewData["PecaFk"] = new SelectList(_context.Pecas, "Id", "Nome", pecasFotos.PecaFk);
            return View(pecasFotos);
        }

        // GET: PecasFotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecasFotos = await _context.PecasFotos
                .Include(p => p.Fotografia)
                .Include(p => p.Peca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pecasFotos == null)
            {
                return NotFound();
            }

            return View(pecasFotos);
        }

        // POST: PecasFotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pecasFotos = await _context.PecasFotos.FindAsync(id);
            _context.PecasFotos.Remove(pecasFotos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PecasFotosExists(int id)
        {
            return _context.PecasFotos.Any(e => e.Id == id);
        }
    }
}
