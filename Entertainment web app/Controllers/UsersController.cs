using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Services;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Controllers;


[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<UserDto>> GetAll()
    {
        try
        {
            var users = await _userService.GetAll();

            if (users.IsFailure)
            {
                return new Response<UserDto>
                {
                    Status = "error",
                    Error = users.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new Response<UserDto>
            {
                Status = "success",
                Data = users.Data,
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response<UserDto>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<UserDto>> GetById(string userId)
    {
        try
        {
            var user = await _userService.GetById(userId);

            if (user.IsFailure)
            {
                return new Response<UserDto>
                {
                    Status = "error",
                    Error = user.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new Response<UserDto>
            {
                Status = "success",
                Data = new List<UserDto> { user.Data! },
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response<UserDto>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }

    [HttpPut("{userId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<UserDto>> Update(string userId, [FromBody] UserDto userDto)
    {
        try
        {
            userDto.Id = userId;
            var result = await _userService.Update(userDto);

            if (result.IsFailure)
            {
                return new Response<UserDto>
                {
                    Status = "error",
                    Error = result.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new Response<UserDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response<UserDto>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<UserDto>> Delete(string userId)
    {
        try
        {
            var result = await _userService.Delete(userId);

            if (result.IsFailure)
            {
                return new Response<UserDto>
                {
                    Status = "error",
                    Error = result.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new Response<UserDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response<UserDto>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }
}
