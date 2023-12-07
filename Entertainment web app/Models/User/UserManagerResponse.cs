namespace Entertainment_web_app.Models.User;

public class UserManagerResponse
{
    public string Message { get; set; }
    public bool isSuccess { get; set; }
    public IEnumerable<string> Errors { get; set; }
    public DateTime? ExpireDate { get; set; }
}