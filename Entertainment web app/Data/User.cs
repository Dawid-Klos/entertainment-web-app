using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entertainment_web_app.Data;

public class User
{
    [Key] 
    public int UserId { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [PasswordPropertyText]
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    [StringLength(20)]
    public string Firstname { get; set; }
    [StringLength(20)]
    public string Lastname { get; set; }

}