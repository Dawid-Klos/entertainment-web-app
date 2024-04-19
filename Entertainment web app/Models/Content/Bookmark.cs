using Entertainment_web_app.Models.User;

namespace Entertainment_web_app.Models.Content;

public class Bookmark
{
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
}
