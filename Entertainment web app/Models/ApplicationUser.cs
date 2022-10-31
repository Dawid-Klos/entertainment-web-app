using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entertainment_web_app.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(20)]
    public string? Firstname { get; set; }
    [Required]
    [StringLength(20)]
    public string? Lastname { get; set; }
    
}