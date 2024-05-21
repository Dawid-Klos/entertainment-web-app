namespace Entertainment_web_app.Models.Dto;

public class SearchQuery
{
    public string? Title { get; set; }
    public int? Year { get; set; }
    public string? Rating { get; set; }
    public string? SortBy { get; set; }
    public bool IsAscending { get; set; }
}
