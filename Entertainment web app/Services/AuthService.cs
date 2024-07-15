using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using Entertainment_web_app.Models.Auth;
using Entertainment_web_app.Data;
using Entertainment_web_app.Common.Responses;

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

    public Result AuthenticateUser()
    {
        var userContext = _httpContextAccessor.HttpContext;

        if (userContext?.User.Identity == null)
        {
            return Result.Failure(new Error("Unauthorized", "User is not authenticated"));
        }

        var userToken = userContext.Request.Cookies["_auth"];
        var tokenHandler = new JwtSecurityTokenHandler();

        tokenHandler.ValidateToken(userToken, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = _configuration["JWT_AUDIENCE"],
            ValidIssuer = _configuration["JWT_ISSUER"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_SECRET_KEY"]!)),
        }, out SecurityToken validatedToken);

        if (validatedToken == null)
        {
            return Result.Failure(new Error("Unauthorized", "Invalid token"));
        }

        return Result.Success();
    }

    public async Task<Result> RegisterUser(RegisterViewModel model)
    {

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {
            return Result.Failure(new Error("BadRequest", "User with that email address already exists"));
        }

        if (model.Password != model.ConfirmPassword)
        {
            return Result.Failure(new Error("BadRequest", "Passwords do not match"));
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
            var errors = result.Errors.Select(error => error.Description).Aggregate((error1, error2) => $"{error1}, {error2}");
            return Result.Failure(new Error("BadRequest", $"User creation failed: {errors}"));
        }

        var userRole = await _userManager.AddToRoleAsync(applicationUser, "User");

        if (!userRole.Succeeded)
        {
            return Result.Failure(new Error("BadRequest", "User role assignment failed"));
        }

        return Result.Success();
    }


    public async Task<Result> LoginUser(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return Result.Failure(new Error("BadRequest", "User not found"));
        }

        var result = await _userManager.CheckPasswordAsync(user, model.Password);

        if (!result)
        {
            return Result.Failure(new Error("BadRequest", "Invalid password"));
        }

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, model.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var keyString = _configuration["JWT_SECRET_KEY"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT_ISSUER"],
            audience: _configuration["JWT_AUDIENCE"],
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
            return Result.Failure(new Error("InternalError", "An error occurred while processing your request."));
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append("_auth", tokenAsString, cookieOptions);

        return Result.Success();
    }

    public Result LogoutUser()
    {

        var userContext = _httpContextAccessor.HttpContext;

        if (userContext?.User.Identity == null || !userContext.User.Identity.IsAuthenticated)
        {
            return Result.Failure(new Error("Unauthorized", "User is not authenticated"));
        }

        userContext.Response.Cookies.Delete("_auth");

        return Result.Success();
    }
}
