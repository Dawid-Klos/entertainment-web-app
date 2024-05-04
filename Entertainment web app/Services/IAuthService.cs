using Entertainment_web_app.Models.Auth;
using Entertainment_web_app.Models.User;
using Entertainment_web_app.Models.Responses;

namespace Entertainment_web_app.Services;

public interface IAuthService
{
    Task<Response<ApplicationUser>> RegisterUserAsync(RegisterViewModel model);
    Task<Response<ApplicationUser>> LoginUserAsync(LoginViewModel model);
    Task<Response<ApplicationUser>> LogoutUserAsync();
    Task<Response<ApplicationUser>> UpdateUserAsync(ApplicationUser user);
}
