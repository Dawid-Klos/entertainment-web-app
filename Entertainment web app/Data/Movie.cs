using System.ComponentModel.DataAnnotations;

namespace Entertainment_web_app.Data;

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
    public bool IsTrading { get; set; }
    [StringLength(50)]
    public string ImgTrendingSmall { get; set; }
    [StringLength(50)]
    public string ImgTrendingLarge { get; set; }
    [StringLength(50)]
    public string ImgSmal { get; set; }
    [StringLength(50)]
    public string ImgMedium { get; set; }
    [StringLength(50)]
    public string ImgLarge { get; set; }
}