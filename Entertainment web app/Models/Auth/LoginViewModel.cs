using System.ComponentModel.DataAnnotations;

namespace Entertainment_web_app.Models.Auth;

public class LoginViewModel
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    [StringLength(50, MinimumLength = 8)]
    public string Password { get; set; } = null!;
}
