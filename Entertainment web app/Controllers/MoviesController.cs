using Entertainment_web_app.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entertainment_web_app.Controllers;

[ApiController]
// [Authorize]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly NetwixDbContext _context;

    public MoviesController(NetwixDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllMovies()
    {
        // var accessToken = HttpContext.GetTokenAsync("access_token");
        
        var movies = await _context.Movies.FromSqlRaw("SELECT * FROM \"Movies\" WHERE \"Category\" = 'Movie'").ToListAsync();
        return new JsonResult(movies);
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Search([FromQuery] string search,[FromQuery]string? category)
    {
        // var accessToken = HttpContext.GetTokenAsync("access_token");
        
        var searchString = search;
        var categoryString = category;
        var movie = await _context.Movies
.FromSqlRaw($"SELECT * FROM \"Movies\" WHERE \"Title\" LIKE '%{searchString}%' AND \"Category\" LIKE '%{categoryString}%'").ToListAsync();
        return new JsonResult(movie);
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTrendingMovies()
    {
        // var accessToken = HttpContext.GetTokenAsync("access_token");
        
        var trendingMovies =
            await _context.Movies.FromSqlRaw("SELECT * FROM \"Movies\" WHERE \"IsTrending\" = 'True' ").ToListAsync();
        
        return new JsonResult(trendingMovies);
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTvSeries()
    {
        // var accessToken = HttpContext.GetTokenAsync("access_token");
        
        var tvSeries =
            await _context.Movies.FromSqlRaw("SELECT * FROM \"Movies\" WHERE \"Category\" = 'TV Series'").ToListAsync();
        return new JsonResult(tvSeries);
    }

    // [HttpGet]
    // [Route("[action]")]
    // public async Task<IActionResult> GetBookmarkedMovies(string userId)
    // {
    //     // var user = await _context.Users.FindAsync(userId);
    //     var user = await _context.Users.FindAsync(userId);
    //     var bookmarkedMovies = _context.Movies.FromSqlRaw(
    //         "SELECT * FROM Movies INNER JOIN ApplicationUserMovie ON ApplicationUserMovie.MoviesMovieId = AspNetUsers.Id WHERE isBookmarked = 1").ToListAsync();
    //
    //     return new JsonResult(bookmarkedMovies);
    // }
}