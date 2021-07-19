using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JuFlores.Data;
using JuFlores.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JuFlores.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Utilizadores> _userManager;
        private readonly SignInManager<Utilizadores> _signInManager;
        private readonly JuFloresDB _context;

        public IndexModel(
            UserManager<Utilizadores> userManager,
            SignInManager<Utilizadores> signInManager,
            JuFloresDB context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Morada")]
            public string Morada { get; set; }
        }

        private async Task LoadAsync(Utilizadores user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var morada = _context.Utilizadores.Where(utilizador => utilizador.Email == User.Identity.Name).First().Morada;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Morada = morada
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            // Verificar se a morada é diferente e atualizar
            Utilizadores utilizador = _context.Utilizadores.Where(utilizador => utilizador.Email == User.Identity.Name).First();
            if(Input.Morada != utilizador.Morada)
            {
                utilizador.Morada = Input.Morada;
                _context.Update(utilizador);
                await _context.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
