using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JuFlores.Data;
using JuFlores.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace JuFlores.Controllers
{
    public class FotografiasController : Controller
    {
        private readonly JuFloresDB _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FotografiasController(JuFloresDB context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Fotografias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fotografias.ToListAsync());
        }

        // GET: Fotografias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografias = await _context.Fotografias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotografias == null)
            {
                return NotFound();
            }

            return View(fotografias);
        }

        // GET: Fotografias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fotografias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fotografia,FicheiroImagem")] Fotografias fotografias)
        {
            if (ModelState.IsValid)
            {
                // Guardar imagem no wwwroot/imagens
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(fotografias.FicheiroImagem.FileName);
                string extention = Path.GetExtension(fotografias.FicheiroImagem.FileName);
                fileName += DateTime.Now.ToString("yymmss") + extention;
                string path = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await fotografias.FicheiroImagem.CopyToAsync(fileStream);
                }

                fotografias.Fotografia = fileName;
                _context.Add(fotografias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fotografias);
        }


        // GET: Fotografias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografias = await _context.Fotografias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotografias == null)
            {
                return NotFound();
            }

            return View(fotografias);
        }

        // POST: Fotografias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotografias = await _context.Fotografias.FindAsync(id);
            _context.Fotografias.Remove(fotografias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotografiasExists(int id)
        {
            return _context.Fotografias.Any(e => e.Id == id);
        }
    }
}
