using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using Entertainment_web_app.Models.User;
using Entertainment_web_app.Models.Auth;
using Entertainment_web_app.Models.Responses;

namespace Entertainment_web_app.Services;

public class AuthService : IAuthService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response<ApplicationUser>> RegisterUserAsync(RegisterViewModel model)
    {
        if (model == null)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                Error = "Register model is null",
            };
        }

        if (model.Password != model.ConfirmPassword)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                Error = "Confirm password doesn't match the password",
            };
        }

        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                Error = "User with that email address already exists",
            };
        }

        var applicationUser = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Email,
            Firstname = model.Firstname,
            Lastname = model.Lastname
        };

        var result = await _userManager.CreateAsync(applicationUser, model.Password);

        if (!result.Succeeded)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                Error = "User creation failed!",
            };
        }

        return new Response<ApplicationUser>
        {
            Status = "success",
            Data = new List<ApplicationUser>() { applicationUser }
        };
    }


    public async Task<Response<ApplicationUser>> LoginUserAsync(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                Error = "User with that email address does not exist",
            };
        }

        var result = await _userManager.CheckPasswordAsync(user, model.Password);

        if (!result)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                Error = "Invalid password",
            };
        }

        var claims = new[]
        {
        new Claim("Email", model.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
      };

        var keyString = _configuration["AuthSettings:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString!));

        var token = new JwtSecurityToken(
            issuer: _configuration["AuthSettings:Issuer"],
            audience: _configuration["AuthSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(1)
        };

        if (_httpContextAccessor.HttpContext == null)
        {
            return new Response<ApplicationUser>
            {
                Status = "error",
                Error = "HttpContext is null"
            };
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append("_auth", tokenAsString, cookieOptions);

        return new Response<ApplicationUser>
        {
            Status = "success",
            Data = new List<ApplicationUser>() { user }
        };
    }

    public Task<Response<ApplicationUser>> LogoutUserAsync()
    {

        if (_httpContextAccessor.HttpContext?.User.Identity == null)
        {
            return Task.FromResult(new Response<ApplicationUser>
            {
                Status = "error",
                Error = "HttpContext is null"
            });
        }

        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {

            return Task.FromResult(new Response<ApplicationUser>
            {
                Status = "error",
                Error = "User is not authenticated"
            });
        }

        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("_auth");

        return Task.FromResult(new Response<ApplicationUser>
        {
            Status = "success",
        });
    }

    public Task<Response<ApplicationUser>> UpdateUserAsync(ApplicationUser user)
    {
        var userToUpdate = _userManager.FindByIdAsync(user.Id).Result;

        if (userToUpdate == null)
        {
            return Task.FromResult(new Response<ApplicationUser>
            {
                Status = "error",
                Error = "User not found"
            });
        }

        userToUpdate.Firstname = user.Firstname;
        userToUpdate.Lastname = user.Lastname;

        var result = _userManager.UpdateAsync(userToUpdate).Result;

        if (!result.Succeeded)
        {
            return Task.FromResult(new Response<ApplicationUser>
            {
                Status = "error",
                Error = "User update failed"
            });
        }

        return Task.FromResult(new Response<ApplicationUser>
        {
            Status = "success",
            Data = new List<ApplicationUser>() { userToUpdate }
        });
    }

    public Task<Response<ApplicationUser>> DeleteUserAsync(string userId)
    {
        var user = _userManager.FindByIdAsync(userId).Result;

        if (user == null)
        {
            return Task.FromResult(new Response<ApplicationUser>
            {
                Status = "error",
                Error = "User not found"
            });
        }

        var result = _userManager.DeleteAsync(user).Result;

        if (!result.Succeeded)
        {
            return Task.FromResult(new Response<ApplicationUser>
            {
                Status = "error",
                Error = "User deletion failed"
            });
        }

        return Task.FromResult(new Response<ApplicationUser>
        {
            Status = "success",
        });
    }

}
