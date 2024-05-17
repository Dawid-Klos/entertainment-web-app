namespace Entertainment_web_app.Models.Dto;

public class MovieDto
{
    public int MovieId { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string Category { get; set; } = null!;
    public string Rating { get; set; } = null!;
    public string ImgSmall { get; set; } = null!;
    public string ImgMedium { get; set; } = null!;
    public string ImgLarge { get; set; } = null!;
    public bool? IsBookmarked { get; set; }
}

