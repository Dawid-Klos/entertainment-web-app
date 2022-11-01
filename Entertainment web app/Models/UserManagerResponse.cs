namespace Entertainment_web_app.Models;

public class UserManagerResponse
{
    public string Message { get; set; }
    public bool isSuccess { get; set; }
    public IEnumerable<string> Errors { get; set; }
}