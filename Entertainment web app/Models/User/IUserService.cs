using Entertainment_web_app.Models.Auth;

namespace Entertainment_web_app.Models.User;

public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
        Task<UserManagerResponse> LogoutUserAsync();
    }
    
