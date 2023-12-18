using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using Entertainment_web_app.Models.Auth;

namespace Entertainment_web_app.Models.User;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
    {
        if (model == null)
        {
            throw new NullReferenceException("Register model is null");
        }

        if (model.Password != model.ConfirmPassword)
        {
            return new UserManagerResponse
            {
                Message = "Confirm password doesn't match the password",
                isSuccess = false
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
            return new UserManagerResponse
            {
                Message = "User has not been created",
                isSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        return new UserManagerResponse
        {
            Message = "User has been created successfully!",
            isSuccess = true
        };
    }

    public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return new UserManagerResponse
            {
                Message = "User with that email address doesn't exist",
                isSuccess = false
            };
        }

        var result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result)
        {
            return new UserManagerResponse
            {
                Message = "Invalid password",
                isSuccess = false
            };
        }

        var claims = new[]
        {
            new Claim("Email", model.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        
        var keyString = _configuration["AuthSettings:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

        var token = new JwtSecurityToken(
            issuer: _configuration["AuthSettings:Issuer"],
            audience: _configuration["AuthSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        
        string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(30)
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("_auth", tokenAsString, cookieOptions);

        return new UserManagerResponse
        {
            Message = tokenAsString,
            isSuccess = true,
            ExpireDate = token.ValidTo
        };
    }
}