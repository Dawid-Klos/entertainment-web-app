using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Auth;
using Entertainment_web_app.Repositories;

namespace Entertainment_web_app.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _userRepository.GetAll();

        if (users is null)
        {
            return Result<IEnumerable<UserDto>>.Failure(new Error("NotFound", "No users found"));
        }

        var userDtos = users.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname
        });

        return Result<IEnumerable<UserDto>>.Success(userDtos);
    }

    public async Task<Result<UserDto>> GetById(string userId)
    {
        var user = await _userRepository.GetById(userId);

        if (user is null)
        {
            return Result<UserDto>.Failure(new Error("NotFound", "User not found"));
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname
        };

        return Result<UserDto>.Success(userDto);
    }

    public async Task<Result> Update(UserDto userDto)
    {
        var user = await _userRepository.GetById(userDto.Id);

        if (user is null)
        {
            return Result.Failure(new Error("NotFound", "User not found"));
        }

        user.UserName = userDto.UserName;
        user.Email = userDto.Email;
        user.Firstname = userDto.Firstname;
        user.Lastname = userDto.Lastname;

        await _userRepository.Update(user);

        return Result.Success();
    }

    public async Task<Result> Delete(string userId)
    {
        var user = await _userRepository.GetById(userId);

        if (user is null)
        {
            return Result.Failure(new Error("NotFound", "User not found"));
        }

        await _userRepository.Delete(user);

        return Result.Success();
    }
}
