using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Auth;
using Entertainment_web_app.Services;

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
    [Authorize(Roles = "User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Response Auth()
    {
        try
        {
            var authStatus = _authService.AuthenticateUser();

            if (authStatus.IsFailure)
            {
                return new Response
                {
                    Status = "error",
                    Error = authStatus.Error,
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }


            return new Response
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Response> RegisterAsync([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return new Response
            {
                Status = "error",
                StatusCode = StatusCodes.Status400BadRequest,
                Error = new Error("BadRequest", "Invalid model state")
            };
        }

        try
        {
            var result = await _authService.RegisterUser(model);

            if (result.IsFailure)
            {
                return new Response
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = result.Error
                };
            }

            return new Response
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Response> LoginAsync([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return new Response
            {
                Status = "error",
                StatusCode = StatusCodes.Status400BadRequest,
                Error = new Error("BadRequest", "Invalid model state")
            };
        }

        try
        {
            var result = await _authService.LoginUser(model);

            if (result.IsFailure)
            {
                return new Response
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = result.Error
                };
            }

            return new Response
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }

    [HttpPost("logout")]
    [Authorize(Roles = "User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Response Logout()
    {
        try
        {
            var result = _authService.LogoutUser();

            if (result.IsFailure)
            {
                return new Response
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = result.Error
                };
            }

            return new Response
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }
}
