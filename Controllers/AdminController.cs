using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projet2Auth.Models;

namespace Projet2Exp.Controllers
{
    [Authorize(Roles = "Admin")] //Admin seulement
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Liste de tous les utilisateurs
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}