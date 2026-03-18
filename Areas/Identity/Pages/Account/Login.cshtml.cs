using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Projet2Auth.Areas.Identity.Pages.Account
{
    public class LoginModel(SignInManager<IdentityUser> signInManager) : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;

        [BindProperty]
        public InputModel? Input { get; set; }

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Le courriel est obligatoire.")]
            [EmailAddress(ErrorMessage = "Format de courriel invalide.")]
            [Display(Name = "Courriel")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string? Password { get; set; }

            [Display(Name = "Se souvenir de moi")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        
    }
}