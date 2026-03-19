using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projet2Auth.Models;
using System.ComponentModel.DataAnnotations;

namespace Projet2Auth.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
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
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

            [Required(ErrorMessage = "La confirmation est obligatoire.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
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
                var user = new AppUser
                {
                    UserName = Input?.Email,
                    Email = Input?.Email
                };

                var result = await _userManager.CreateAsync(user, Input?.Password!);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}