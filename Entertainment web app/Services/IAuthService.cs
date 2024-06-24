using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Auth;

namespace Entertainment_web_app.Services;

public interface IAuthService
{
    Result AuthenticateUser();
    Result LogoutUser();
    Task<Result> RegisterUser(RegisterViewModel model);
    Task<Result> LoginUser(LoginViewModel model);
}
