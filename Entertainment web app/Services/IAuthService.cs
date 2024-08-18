using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Auth;
using Entertainment_web_app.Models.Dto;

namespace Entertainment_web_app.Services;

public interface IAuthService
{
    Result<UserDto> AuthenticateUser();
    Result LogoutUser();
    Task<Result> RegisterUser(RegisterViewModel model);
    Task<Result> LoginUser(LoginViewModel model);
}
