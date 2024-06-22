using Entertainment_web_app.Data;
using Entertainment_web_app.Models.Auth;
using Entertainment_web_app.Common.Responses;

namespace Entertainment_web_app.Services;

public interface IAuthService
{
    Result AuthenticateUser();
    Result LogoutUser();
    Task<Result> RegisterUser(RegisterViewModel model);
    Task<Result> LoginUser(LoginViewModel model);
    Task<Result> UpdateUser(ApplicationUser user);
    Task<Result> DeleteUser(string userId);
    Task<Result> AddRoleToUser(string userId, string roleName);
    Task<Result> RemoveRoleFromUser(string userId, string roleName);
}
