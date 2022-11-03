using Entertainment_web_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
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

    // [HttpPost("Login")]
    // public async Task<IActionResult> LoginAsync([FromBody])
    // {
    //     
    // }
}