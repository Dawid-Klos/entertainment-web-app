
namespace Entertainment_web_app.Models.Dto;

public class TrendingDto
{
    public int MovieId { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string Category { get; set; } = null!;
    public string Rating { get; set; } = null!;
    public string ImgTrendingSmall { get; set; } = null!;
    public string ImgTrendingLarge { get; set; } = null!;
    public bool? IsBookmarked { get; set; }
}

