using System.ComponentModel.DataAnnotations;
using Entertainment_web_app.Models.User;

namespace Entertainment_web_app.Models.Content;

public class Movie
{
    [Key]
    public int MovieId { get; set; }
    [StringLength(30)]
    public string Title { get; set; }
    public int Year { get; set; }
    [StringLength(10)]
    public string Category { get; set; }
    [StringLength(10)]
    public string Rating { get; set; }
    [StringLength(80)]
    public string ImgSmall { get; set; }
    [StringLength(80)]
    public string ImgMedium { get; set; }
    [StringLength(80)]
    public string ImgLarge { get; set; }
    public ICollection<ApplicationUser> Users { get; } = new List<ApplicationUser>();
}