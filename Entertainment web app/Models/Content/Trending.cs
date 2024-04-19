using System.ComponentModel.DataAnnotations;

namespace Entertainment_web_app.Models.Content;

public class Trending
{
    [Key]
    public int TrendingId { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required]
    [StringLength(80)]
    public string ImgTrendingSmall { get; set; } = null!;

    [Required]
    [StringLength(80)]
    public string ImgTrendingLarge { get; set; } = null!;

    public Movie Movie { get; set; } = null!;
}
