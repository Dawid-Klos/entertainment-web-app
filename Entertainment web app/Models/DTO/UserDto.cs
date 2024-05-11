namespace Entertainment_web_app.Models.Auth;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
}
