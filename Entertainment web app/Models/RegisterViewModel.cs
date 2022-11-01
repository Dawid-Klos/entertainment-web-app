using System.ComponentModel.DataAnnotations;

namespace Entertainment_web_app.Models;

public class RegisterViewModel
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 8)]
    public string Password { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 8)]
    public string ConfirmPassword { get; set; }
    [Required]
    [StringLength(20)]
    public string Firstname { get; set; }
    [Required]
    [StringLength(20)]
    public string Lastname { get; set; }
}