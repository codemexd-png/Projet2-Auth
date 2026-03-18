using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projet2Auth.Data;
using Projet2Auth.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//Connexion à la  Base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Activaation du register et du login ainsi que le logout
//Doit confirmer l'email pour se connecter
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() //Ajout de role pour identity
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

/*
    Permet de créer automatiquement  Les rôles et la gestion des users 
 */
using (var scope = app.Services.CreateScope())
{
    //Accès au services d'asp
    var services = scope.ServiceProvider;

    //Permet de gérer les rôles 
    var ManagerRole = services.GetRequiredService<RoleManager<IdentityRole>>();
    //Pemet de gérer les utilisateurs 
    var ManagerUser = services.GetRequiredService<UserManager<AppUser>>();


    //Définition de nos rôles 
    string[] Mesroles = { "Admin", "User" };

    foreach (var role in Mesroles)
    {
        //Verifie  le role existe dejà dans la bdd 
        if (!await ManagerRole.RoleExistsAsync(role))
        {
            //Si le rôle n'existe pas on le crée 
            await ManagerRole.CreateAsync(new IdentityRole(role));
        }
    }

    //Compte administrateur 
    var EmailAdm = "admin@tester.com";
    var PasswordAdm = "Admin12345!";

    //Vérifie si un user a déjà l'email
    var adminUser = await ManagerUser.FindByEmailAsync(EmailAdm);

    //Si on n'a aucun user admin 
    if (adminUser == null)
    {
        //on crée un new admin 
        var user = new AppUser
        {
            UserName = EmailAdm,
            Email = EmailAdm,
            EmailConfirmed = true
        };


        //Création du suer dans la bdd et son password 

        var result = await ManagerUser.CreateAsync(user, PasswordAdm);


        // Création user réussi 
        if (result.Succeeded)
        {
            //ajout du user avec un role d'admin 
            await ManagerUser.AddToRoleAsync(user, "Admin");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
