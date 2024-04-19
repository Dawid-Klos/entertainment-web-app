using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

using Entertainment_web_app.Models.Content;

namespace Entertainment_web_app.Models.User;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(20)]
    public string? Firstname { get; set; }

    [Required]
    [StringLength(20)]
    public string? Lastname { get; set; }

    public ICollection<Bookmark> Bookmarks { get; } = new List<Bookmark>();
}
