using JuFlores.Data;
using JuFlores.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuFlores.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UtilizadoresController : Controller
    {
        private readonly JuFloresDB _context;
        private readonly UserManager<Utilizadores> _userManager;

        public UtilizadoresController(
            JuFloresDB context,
            UserManager<Utilizadores> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UtilizadoresController
        public async Task<IActionResult> Index()
        {
            var utilizadores = _context.Utilizadores;
            ViewData["UserRoles"] = _context.UserRoles.ToList();

            return View(await utilizadores.ToListAsync());
        }

        // GET: UtilizadoresController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            ViewData["UserRoles"] = _context.UserRoles.Where(role => role.UserId == id).ToList();
            return View(utilizador);
        }

        // GET: UtilizadoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilizadoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UtilizadoresController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            var role = _context.UserRoles.Where(role => role.UserId == id).FirstOrDefault();

            UtilizadoresInputModel utilizadorInput = new()
            {
                Id = utilizador.Id,
                Nome = utilizador.Nome,
                Email = utilizador.Email,
                Morada = utilizador.Morada,
                NumeroTelefone = utilizador.PhoneNumber,
                RoleId = role == null ? "" : role.RoleId,
            };

            return View(utilizadorInput);
        }

        // POST: UtilizadoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Email,Nome,Morada,NumeroTelefone,RoleId")] UtilizadoresInputModel updatedUser)
        {
            if(!id.Equals(updatedUser.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Utilizadores newUser = _context.Utilizadores.Where(user => user.Id == id).FirstOrDefault();
                if (newUser == null)
                {
                    return NotFound();
                }
                newUser.Email = updatedUser.Email;
                newUser.Morada = updatedUser.Morada;
                newUser.Nome = updatedUser.Nome;
                newUser.PhoneNumber = updatedUser.NumeroTelefone;

                // Adicionar ao Role
                var roles = await _userManager.GetRolesAsync(newUser);
                if (updatedUser.RoleId != null && updatedUser.RoleId.Length > 3 && !roles.Any(role => role == updatedUser.RoleId))
                {
                    await _userManager.RemoveFromRolesAsync(newUser, roles); // Remover as roles antigas
                    await _userManager.AddToRoleAsync(newUser, updatedUser.RoleId); // Colocar a nova role
                }
                try
                {
                    _context.Update(newUser);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
