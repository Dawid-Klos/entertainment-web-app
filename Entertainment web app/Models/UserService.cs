using Microsoft.AspNetCore.Identity;

namespace Entertainment_web_app.Models;

public class UserService : IUserService
{
    private UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
    {
        if (model == null)
        {
            throw new NullReferenceException("Register model is null");
        }

        if (model.Password != model.ConfirmPassword)
        {
            return new UserManagerResponse
            {
                Message = "Confirm password doesn't match the password",
                isSuccess = false
            };
        }

        var applicationUser = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Email,
            Firstname = model.Firstname,
            Lastname = model.Lastname
        };

        var result = await _userManager.CreateAsync(applicationUser, model.Password);

        if (!result.Succeeded)
        {
            return new UserManagerResponse
            {
                Message = "User has not been created",
                isSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        return new UserManagerResponse
        {
            Message = "User has been created successfully!",
            isSuccess = true
        };
    }
}