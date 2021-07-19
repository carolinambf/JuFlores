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
    public class FavoritosController : Controller
    {
        private readonly JuFloresDB _context;

        public FavoritosController(JuFloresDB context)
        {
            _context = context;
        }

        // GET: Favoritos
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Administrador") || User.IsInRole("Funcionario"))
            {
                var juFloresDB = _context.Favoritos.Include(f => f.Artigo).Include(f => f.Utilizador);
                return View(await juFloresDB.ToListAsync());
            }

            // Se for um user normal retorna apenas os seus favoritos
            var Favoritos = _context.Favoritos
            .Where(fav => fav.UtilizadorFK == _context.Utilizadores.Where(utilizador => utilizador.Email == User.Identity.Name).First().Id)
            .Include(f => f.Artigo);

            return View(Favoritos.ToList());
        }

        // GET: Favoritos/Details/5
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoritos = await _context.Favoritos
                .Include(f => f.Artigo)
                .Include(f => f.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoritos == null)
            {
                return NotFound();
            }

            return View(favoritos);
        }

        // GET: Favoritos/Create
        [Authorize(Roles = "Funcionario,Administrador")]
        public IActionResult Create()
        {
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Criador");
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id");
            ViewData["Utilizadores"] = _context.Utilizadores.ToList();
            return View();
        }

        // POST: Favoritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Create([Bind("Id,UtilizadorFK,Data,ArtigoFK")] Favoritos favoritos)
        {
            if (ModelState.IsValid)
            {
                if(!_context.Favoritos.Any(favorito =>
                favorito.UtilizadorFK == favoritos.UtilizadorFK &&
                favorito.ArtigoFK == favoritos.ArtigoFK)) 
                {
                    _context.Add(favoritos);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Criador", favoritos.ArtigoFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", favoritos.UtilizadorFK);
            return View(favoritos);
        }

        // GET: Favoritos/Edit/5
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoritos = await _context.Favoritos.FindAsync(id);
            if (favoritos == null)
            {
                return NotFound();
            }
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Criador", favoritos.ArtigoFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", favoritos.UtilizadorFK);
            return View(favoritos);
        }

        // POST: Favoritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Funcionario,Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UtilizadorFK,Data,ArtigoFK")] Favoritos favoritos)
        {
            if (id != favoritos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoritos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoritosExists(favoritos.Id))
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
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Criador", favoritos.ArtigoFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", favoritos.UtilizadorFK);
            return View(favoritos);
        }


        // POST: Favoritos/Toggle/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            int ArtigoId = Id;
            Utilizadores user = _context.Utilizadores.Where(utilizador => utilizador.Email == User.Identity.Name).FirstOrDefault();
            Artigos artigo = _context.Artigos.Where(artigo => artigo.Id == ArtigoId).FirstOrDefault();

            if (user == null || artigo == null)
            {
                return NotFound();
            }

            Favoritos favorito = _context.Favoritos.Where(fav => fav.UtilizadorFK == user.Id && fav.ArtigoFK == ArtigoId).FirstOrDefault();
            // Apagar se o favorito existir, criar se não existir
            if (favorito == null) // Não existe
            {
                var result = await Create(new Favoritos()
                {
                    ArtigoFK = ArtigoId,
                    Artigo = artigo,
                    Data = DateTime.Now,
                    Utilizador = user,
                    UtilizadorFK = user.Id
                });
                return RedirectToAction(nameof(Index));
            }

            // Existe
            var result2 = await DeleteConfirmed(favorito.Id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Favoritos/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Favoritos favorito = await _context.Favoritos
                .Include(f => f.Artigo)
                .Include(f => f.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favorito == null)
            {
                return NotFound();
            }
            if (!User.IsInRole("Administrador") || !User.IsInRole("Funcionario"))
            {
                //Se for cliente verificar se o favorito lhe pertence
                if(favorito.UtilizadorFK != _context.Utilizadores.Where(utilizador => utilizador.Email == User.Identity.Name).FirstOrDefault().Id)
                {
                    return Unauthorized();
                }
            }

            return View(favorito);
        }

        // POST: Favoritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favoritos = await _context.Favoritos.FindAsync(id);
            _context.Favoritos.Remove(favoritos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoritosExists(int id)
        {
            return _context.Favoritos.Any(e => e.Id == id);
        }
    }
}
