using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Entertainment_web_app.Services;
using Entertainment_web_app.Models.User;
using Entertainment_web_app.Models.Auth;
using Entertainment_web_app.Models.Responses;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Response<ApplicationUser> Auth()
    {
        try
        {
            var authStatus = _authService.AuthenticateUserAsync();

            if (authStatus.Status == "success")
            {
                return new Response<ApplicationUser>
                {
                    Status = "success",
                    StatusCode = StatusCodes.Status200OK,
                    Data = authStatus.Data
                };
            }
        }
        catch (Exception ex)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = ex.Message
            };
        }

        return new Response<ApplicationUser>
        {
            Status = "error",
            StatusCode = StatusCodes.Status401Unauthorized,
            Error = "User is not authenticated"
        };
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Response<ApplicationUser>> RegisterAsync([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                StatusCode = StatusCodes.Status400BadRequest,
                Error = "Something went wrong."
            };
        }

        try
        {
            var result = await _authService.RegisterUserAsync(model);

            if (result.Status != "success")
            {
                return new Response<ApplicationUser>
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = result.Error
                };
            }

            return new Response<ApplicationUser>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = result.Data
            };
        }
        catch (Exception ex)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = ex.Message
            };
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Response<ApplicationUser>> LoginAsync([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                StatusCode = StatusCodes.Status400BadRequest,
                Error = "Something went wrong."
            };
        }

        try
        {
            var result = await _authService.LoginUserAsync(model);

            if (result.Status != "success")
            {
                return new Response<ApplicationUser>
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = result.Error
                };
            }

            return new Response<ApplicationUser>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = result.Data
            };
        }
        catch (Exception ex)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = ex.Message
            };
        }
    }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<ApplicationUser>> LogoutAsync()
    {

        try
        {
            var result = await _authService.LogoutUserAsync();

            if (result.Status != "success")
            {
                return new Response<ApplicationUser>
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = result.Error
                };
            }

            return new Response<ApplicationUser>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = result.Data
            };
        }
        catch (Exception ex)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = ex.Message
            };
        }
    }

}
