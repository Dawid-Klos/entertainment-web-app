using Entertainment_web_app.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Entertainment_web_app.Models.User;


namespace Entertainment_web_app.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private IUserService _userService;
    private IHttpContextAccessor _httpContextAccessor;

    public AuthController(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    [Authorize]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public JsonResult Auth()
    {
        var authStatus = _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;

        if (authStatus)
        {
            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = "User is authenticated" });
        }

        return new JsonResult(new { status = "error", statusCode = StatusCodes.Status401Unauthorized, error = "User is not authenticated" });
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.RegisterUserAsync(model);

            if (result.isSuccess)
            {
                return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = result });
            }

            if (!result.isSuccess)
            {
                return new JsonResult(new { status = "error", statusCode = StatusCodes.Status400BadRequest, error = result });
            }
        }

        return new JsonResult(new { status = "error", statusCode = StatusCodes.Status400BadRequest, error = "Something went wrong." });
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.LoginUserAsync(model);

            if (result.isSuccess)
            {
                return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = result });
            }

            if (!result.isSuccess)
            {
                return new JsonResult(new { status = "error", statusCode = StatusCodes.Status400BadRequest, error = result });
            }
        }

        return new JsonResult(new { status = "error", statusCode = StatusCodes.Status400BadRequest, error = "Some of the credentials does not match." });
    }

    [HttpPost]
    [Authorize]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LogoutAsync()
    {
        var result = await _userService.LogoutUserAsync();

        if (result.isSuccess)
        {
            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = result });
        }

        if (!result.isSuccess)
        {
            return new JsonResult(new { status = "error", statusCode = StatusCodes.Status400BadRequest, error = result });
        }

        return new JsonResult(new { status = "error", statusCode = StatusCodes.Status400BadRequest, error = "Something went wrong, user not logged out." });
    }

}
