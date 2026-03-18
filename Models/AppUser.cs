using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Projet2Auth.Models;

public class AppUser : IdentityUser
{
    //Ibrahima Phase 
    public string? NomUser { get; set; }
   
   
}

