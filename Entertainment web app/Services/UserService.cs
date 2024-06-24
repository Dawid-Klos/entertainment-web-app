using Microsoft.AspNetCore.Identity;

using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Repositories;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _userRepository.GetAll();

        if (!users.Any() || users is null)
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


    public async Task<Result> AddUserToRole(UserRoleViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            return Result.Failure(new Error("NotFound", "User not found"));
        }

        var result = await _userManager.AddToRoleAsync(user, model.RoleName);

        if (!result.Succeeded)
        {
            return Result.Failure(new Error("BadRequest", "Role assignment failed"));
        }

        return Result.Success();
    }

    public async Task<Result> RemoveUserFromRole(UserRoleViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            return Result.Failure(new Error("NotFound", "User not found"));
        }

        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

        if (!result.Succeeded)
        {
            return Result.Failure(new Error("BadRequest", "Role removal failed"));
        }

        return Result.Success();
    }
}
