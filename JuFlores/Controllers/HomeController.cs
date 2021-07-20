using JuFlores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JuFlores.Data;
using Microsoft.AspNetCore.Authorization;

namespace JuFlores.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly JuFloresDB _context;
        /*
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        */
        public HomeController(JuFloresDB context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Categorias()
        {
            return View();
        }
        public IActionResult Trabalhos()
        {
            ViewData["Artigos"] = _context.Artigos.ToList();
            var fotosArtigo = _context.ArtigosFotos
                .Include(p => p.Fotografia)
                .ToList();
            ViewData["FotosArtigo"] = fotosArtigo;
            ViewData["ArtigosFavoritos"] = _context.Favoritos.Include(f => f.Artigo)
                .Where(favorito => favorito.UtilizadorFK == _context.Utilizadores.Where(utilizador => utilizador.Email == User.Identity.Name).First().Id);
            return View();
        }
        public IActionResult TrabalhosDetalhe(int? id)
        {
            var artigo = _context.Artigos.Where(artigo => artigo.Id == id).FirstOrDefault();
            if(artigo == null)
            {
                return NotFound();
            }
            ViewData["Artigo"] = artigo;
            var pecasArtigo = _context.PecasArtigos.Where(pecasArtigos => pecasArtigos.ArtigoFK == artigo.Id)
                .Include(p => p.Peca)
                .ToList();
            ViewData["PecasArtigos"] = pecasArtigo;
            var fotosArtigo = _context.ArtigosFotos.Where(fotosArt => fotosArt.ArtigoFk == artigo.Id)
                .Include(p => p.Fotografia)
                .ToList();
            ViewData["fotosArtigo"] = fotosArtigo;
            List<PecasFotos> pecasFotos = new();
            foreach (var Peca in pecasArtigo)
            {
                var fotoPeca = _context.PecasFotos.Where(pecaFoto => pecaFoto.PecaFk == Peca.PecaFK)
                    .Include(p => p.Fotografia)
                    .FirstOrDefault(); ;
                if(fotoPeca != null)
                {
                    pecasFotos.Add(fotoPeca);
                }
            }
            ViewData["PecasFotos"] = pecasFotos;

            return View();
        }
        public IActionResult Favoritos()
        {
            return View();
        }
        [Authorize]
        public IActionResult Administracao()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
