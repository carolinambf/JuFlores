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
            ViewData["ArtigosFavoritos"] = _context.Favoritos.Include(f => f.Artigo)
                .Where(favorito => favorito.UtilizadorFK == _context.Utilizadores.Where(utilizador => utilizador.Email == User.Identity.Name).First().Id);
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
