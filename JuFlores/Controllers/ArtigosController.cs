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
    public class ArtigosController : Controller
    {
        private readonly JuFloresDB _context;

        public ArtigosController(JuFloresDB context)
        {
            _context = context;
        }

        // GET: Artigos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artigos.ToListAsync());
        }

        // GET: Artigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigos = await _context.Artigos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigos == null)
            {
                return NotFound();
            }

            return View(artigos);
        }

        // GET: Artigos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artigos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Criador")] Artigos artigos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artigos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artigos);
        }

        // GET: Artigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigos = await _context.Artigos.FindAsync(id);
            if (artigos == null)
            {
                return NotFound();
            }
            return View(artigos);
        }

        // POST: Artigos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Criador")] Artigos artigos)
        {
            if (id != artigos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artigos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtigosExists(artigos.Id))
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
            return View(artigos);
        }

        // GET: Artigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigos = await _context.Artigos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigos == null)
            {
                return NotFound();
            }

            return View(artigos);
        }

        // POST: Artigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artigos = await _context.Artigos.FindAsync(id);
            _context.Artigos.Remove(artigos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtigosExists(int id)
        {
            return _context.Artigos.Any(e => e.Id == id);
        }
    }
}
