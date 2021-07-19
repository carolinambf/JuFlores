using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JuFlores.Data;
using JuFlores.Models;
using Microsoft.AspNetCore.Authorization;

namespace JuFlores.Controllers
{
    [Authorize(Roles = "Funcionario,Administrador")]
    public class PecasArtigosController : Controller
    {
        private readonly JuFloresDB _context;

        public PecasArtigosController(JuFloresDB context)
        {
            _context = context;
        }

        // GET: PecasArtigos
        public async Task<IActionResult> Index()
        {
            var juFloresDB = _context.PecasArtigos.Include(p => p.Artigo).Include(p => p.Peca);
            return View(await juFloresDB.ToListAsync());
        }

        // GET: PecasArtigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecasArtigos = await _context.PecasArtigos
                .Include(p => p.Artigo)
                .Include(p => p.Peca)
                .FirstOrDefaultAsync(m => m.PecaFK == id);
            if (pecasArtigos == null)
            {
                return NotFound();
            }

            return View(pecasArtigos);
        }

        // GET: PecasArtigos/Create
        public IActionResult Create()
        {
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Criador");
            ViewData["PecaFK"] = new SelectList(_context.Pecas, "Id", "Id");
            return View();
        }

        // POST: PecasArtigos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PecaFK,Qtd,ArtigoFK")] PecasArtigos pecasArtigos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pecasArtigos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Criador", pecasArtigos.ArtigoFK);
            ViewData["PecaFK"] = new SelectList(_context.Pecas, "Id", "Id", pecasArtigos.PecaFK);
            return View(pecasArtigos);
        }

        // GET: PecasArtigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecasArtigos = await _context.PecasArtigos.FindAsync(id);
            if (pecasArtigos == null)
            {
                return NotFound();
            }
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Criador", pecasArtigos.ArtigoFK);
            ViewData["PecaFK"] = new SelectList(_context.Pecas, "Id", "Id", pecasArtigos.PecaFK);
            return View(pecasArtigos);
        }

        // POST: PecasArtigos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PecaFK,Qtd,ArtigoFK")] PecasArtigos pecasArtigos)
        {
            if (id != pecasArtigos.PecaFK)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pecasArtigos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PecasArtigosExists(pecasArtigos.PecaFK))
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
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Criador", pecasArtigos.ArtigoFK);
            ViewData["PecaFK"] = new SelectList(_context.Pecas, "Id", "Id", pecasArtigos.PecaFK);
            return View(pecasArtigos);
        }

        // GET: PecasArtigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecasArtigos = await _context.PecasArtigos
                .Include(p => p.Artigo)
                .Include(p => p.Peca)
                .FirstOrDefaultAsync(m => m.PecaFK == id);
            if (pecasArtigos == null)
            {
                return NotFound();
            }

            return View(pecasArtigos);
        }

        // POST: PecasArtigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pecasArtigos = await _context.PecasArtigos.FindAsync(id);
            _context.PecasArtigos.Remove(pecasArtigos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PecasArtigosExists(int id)
        {
            return _context.PecasArtigos.Any(e => e.PecaFK == id);
        }
    }
}
