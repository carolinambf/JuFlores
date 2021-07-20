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
    public class ArtigosFotosController : Controller
    {
        private readonly JuFloresDB _context;

        public ArtigosFotosController(JuFloresDB context)
        {
            _context = context;
        }

        // GET: ArtigosFotos
        public async Task<IActionResult> Index()
        {
            var juFloresDB = _context.ArtigosFotos.Include(a => a.Artigo).Include(a => a.Fotografia);
            return View(await juFloresDB.ToListAsync());
        }

        // GET: ArtigosFotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigosFotos = await _context.ArtigosFotos
                .Include(a => a.Artigo)
                .Include(a => a.Fotografia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigosFotos == null)
            {
                return NotFound();
            }

            return View(artigosFotos);
        }

        // GET: ArtigosFotos/Create
        public IActionResult Create()
        {
            ViewData["ArtigoFk"] = new SelectList(_context.Artigos, "Id", "Nome");
            ViewData["FotografiaFK"] = new SelectList(_context.Fotografias, "Id", "Fotografia");
            return View();
        }

        // POST: ArtigosFotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtigoFk,Data,FotografiaFK")] ArtigosFotos artigosFotos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artigosFotos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtigoFk"] = new SelectList(_context.Artigos, "Id", "Nome", artigosFotos.ArtigoFk);
            ViewData["FotografiaFK"] = new SelectList(_context.Fotografias, "Id", "Fotografia", artigosFotos.FotografiaFK);
            return View(artigosFotos);
        }

        // GET: ArtigosFotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigosFotos = await _context.ArtigosFotos.FindAsync(id);
            if (artigosFotos == null)
            {
                return NotFound();
            }
            ViewData["ArtigoFk"] = new SelectList(_context.Artigos, "Id", "Nome", artigosFotos.ArtigoFk);
            ViewData["FotografiaFK"] = new SelectList(_context.Fotografias, "Id", "Fotografia", artigosFotos.FotografiaFK);
            return View(artigosFotos);
        }

        // POST: ArtigosFotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArtigoFk,Data,FotografiaFK")] ArtigosFotos artigosFotos)
        {
            if (id != artigosFotos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artigosFotos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtigosFotosExists(artigosFotos.Id))
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
            ViewData["ArtigoFk"] = new SelectList(_context.Artigos, "Id", "Nome", artigosFotos.ArtigoFk);
            ViewData["FotografiaFK"] = new SelectList(_context.Fotografias, "Id", "Fotografia", artigosFotos.FotografiaFK);
            return View(artigosFotos);
        }

        // GET: ArtigosFotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigosFotos = await _context.ArtigosFotos
                .Include(a => a.Artigo)
                .Include(a => a.Fotografia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigosFotos == null)
            {
                return NotFound();
            }

            return View(artigosFotos);
        }

        // POST: ArtigosFotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artigosFotos = await _context.ArtigosFotos.FindAsync(id);
            _context.ArtigosFotos.Remove(artigosFotos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtigosFotosExists(int id)
        {
            return _context.ArtigosFotos.Any(e => e.Id == id);
        }
    }
}
