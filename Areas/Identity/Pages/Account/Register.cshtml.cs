using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Projet2Auth.Areas.Identity.Pages.Account
{
    public class RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;

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
            [StringLength(100, ErrorMessage = "Le mot de passe doit avoir au moins {2} caractères.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string? Password { get; set; }

            [Required(ErrorMessage = "La confirmation est obligatoire.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
            [Display(Name = "Confirmer le mot de passe")]
            public string? ConfirmPassword { get; set; }
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input?.Email, Email = Input?.Email };
                var result = await _userManager.CreateAsync(user, Input?.Password!);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                // Affiche les erreurs (ex : email déjà utilisé)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}