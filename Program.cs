using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projet2Auth.Data;
using Projet2Auth.Models;

namespace Projet2Auth
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Home/AccessDenied";
            });

            var app = builder.Build();

            // Seed des rôles et comptes au démarrage
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string[] roles = { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Compte Admin
                var adminEmail = "admin@test.com";
                var adminPassword = "Admin123!";

                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var admin = new AppUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        NomUser = "Administrateur",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(admin, adminPassword);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(admin, "Admin");
                }

                // Compte User
                var userEmail = "user@test.com";
                var userPassword = "User123!";

                if (await userManager.FindByEmailAsync(userEmail) == null)
                {
                    var user = new AppUser
                    {
                        UserName = userEmail,
                        Email = userEmail,
                        NomUser = "Utilisateur Test",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, userPassword);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(user, "User");
                }
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}