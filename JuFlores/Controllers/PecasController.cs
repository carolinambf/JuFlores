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
    public class PecasController : Controller
    {
        private readonly JuFloresDB _context;

        public PecasController(JuFloresDB context)
        {
            _context = context;
        }

        // GET: Pecas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pecas.ToListAsync());
        }

        // GET: Pecas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecas = await _context.Pecas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pecas == null)
            {
                return NotFound();
            }

            return View(pecas);
        }

        // GET: Pecas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pecas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Nome,Tipo,Preco,Descricao,Id,ListaDeFotografias")] Pecas pecas)
        public async Task<IActionResult> Create([Bind("Nome,Tipo,Preco,Descricao,Id,ListaDeFotografias")] PecasViewModel pecas)
        {
            if (ModelState.IsValid)
            {
                // Processar a lista de fotografias
                String[] listaFotosArray = pecas.ListaDeFotografias.Split('\n');
                List < Fotografias > listaFotos = new();
                foreach (var fotoLink in listaFotosArray)
                {
                    listaFotos.Add(new Fotografias() { Fotografia = fotoLink, Date = DateTime.Now});
                }

                Pecas pecasfinal = new(pecas.Nome, pecas.Tipo, pecas.Preco, pecas.Descricao, pecas.Id, listaFotos);
                _context.Add(pecasfinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pecas);
        }

        // GET: Pecas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecas = await _context.Pecas.FindAsync(id);
            if (pecas == null)
            {
                return NotFound();
            }
            return View(pecas);
        }

        // POST: Pecas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Tipo,Preco,Descricao,Id")] Pecas pecas)
        {
            if (id != pecas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pecas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PecasExists(pecas.Id))
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
            return View(pecas);
        }

        // GET: Pecas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecas = await _context.Pecas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pecas == null)
            {
                return NotFound();
            }

            return View(pecas);
        }

        // POST: Pecas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pecas = await _context.Pecas.FindAsync(id);
            _context.Pecas.Remove(pecas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PecasExists(int id)
        {
            return _context.Pecas.Any(e => e.Id == id);
        }
    }
}
