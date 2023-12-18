using Entertainment_web_app.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Entertainment_web_app.Models.User;


namespace Entertainment_web_app.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public Boolean AuthenticateUserAsync()
    {
        var authStatus = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        
        return authStatus;
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.RegisterUserAsync(model);

            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return BadRequest("Some properties are not valid");
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> LoginAsync([FromBody]LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.LoginUserAsync(model);
            if (result.isSuccess)
            {
                return Ok(result);
            }

            if (!result.isSuccess)
            {
                return BadRequest(result);
            }
        }

        return BadRequest("Some properties are not valid");
    }
}