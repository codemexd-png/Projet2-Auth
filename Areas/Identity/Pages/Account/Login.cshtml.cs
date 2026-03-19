using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projet2Auth.Models;
using System.ComponentModel.DataAnnotations;

namespace Projet2Auth.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginModel(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel? Input { get; set; }

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Le courriel est obligatoire.")]
            [EmailAddress(ErrorMessage = "Format de courriel invalide.")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }
    }
}