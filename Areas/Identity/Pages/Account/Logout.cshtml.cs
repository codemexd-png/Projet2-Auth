using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projet2Auth.Models;

namespace Projet2Auth.Areas.Identity.Pages.Account
{
    public class LogoutModel(SignInManager<AppUser> signInManager) : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        public async Task<IActionResult> OnPost(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            returnUrl ??= Url.Content("~/");
            return LocalRedirect(returnUrl);
        }
    }
}