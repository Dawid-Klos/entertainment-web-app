using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Dto;

namespace Entertainment_web_app.Services;

public interface IUserService
{
    Task<Result<IEnumerable<UserDto>>> GetAll();
    Task<Result<UserDto>> GetById(string userId);
    Task<Result> Update(UserDto userDto);
    Task<Result> Delete(string userId);
}
