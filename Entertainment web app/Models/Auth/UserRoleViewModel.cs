using System.ComponentModel.DataAnnotations;

public class UserRoleViewModel
{
    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string RoleName { get; set; } = null!;
}
