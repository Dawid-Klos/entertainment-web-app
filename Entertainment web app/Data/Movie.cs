using System.ComponentModel.DataAnnotations;

namespace Entertainment_web_app.Data;

public class Movie
{
    [Key]
    public int MovieId { get; set; }
    [StringLength(30)]
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    [StringLength(10)]
    public string Category { get; set; } = null!;
    [StringLength(10)]
    public string Rating { get; set; } = null!;
    [StringLength(80)]
    public string ImgSmall { get; set; } = null!;
    [StringLength(80)]
    public string ImgMedium { get; set; } = null!;
    [StringLength(80)]
    public string ImgLarge { get; set; } = null!;
}
