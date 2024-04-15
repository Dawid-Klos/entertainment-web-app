namespace Entertainment_web_app.Models.Content;

public class MovieDto
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Category { get; set; }
    public string Rating { get; set; }
    public string ImgSmall { get; set; }
    public string ImgMedium { get; set; }
    public string ImgLarge { get; set; }
    public bool IsBookmarked { get; set; }
}

