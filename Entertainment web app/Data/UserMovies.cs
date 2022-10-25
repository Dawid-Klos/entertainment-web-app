namespace Entertainment_web_app.Data;
using System.ComponentModel.DataAnnotations;


public class UserMovies
{
    [Key]
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public bool IsBookmarked { get; set; }
}