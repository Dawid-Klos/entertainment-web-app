using Entertainment_web_app.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
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
        try
        {
            var movies = await _context.Movies
                .FromSqlRaw($"SELECT * FROM \"Movies\" WHERE \"Category\" = 'Movies'").ToListAsync();
            
            return new JsonResult(movies);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTrendingMovies()
    {
        try
        {
            var trendingMovies = await _context.Movies.
                FromSqlRaw($"SELECT * FROM \"Movies\" WHERE \"IsTrending\" = 'True' ").ToListAsync();
        
            return new JsonResult(trendingMovies);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTvSeries()
    {
        try
        {
            var tvSeries = await _context.Movies
                .FromSqlRaw($"SELECT * FROM \"Movies\" WHERE \"Category\" = 'TV Series'").ToListAsync();
           
            return new JsonResult(tvSeries);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    // [HttpGet]
    // [Route("[action]")]
    // public async Task<IActionResult> GetBookmarkedMovies(string userId)
    // {
    //     // var user = await _context.Users.FindAsync(userId);
    //     var user = await _context.Users.FindAsync(userId);
    //     var bookmarkedMovies = _context.Movies.FromSqlInterpolated(
    //         "SELECT * FROM Movies INNER JOIN ApplicationUserMovie ON ApplicationUserMovie.MoviesMovieId = AspNetUsers.Id WHERE isBookmarked = 1").ToListAsync();
    //
    //     return new JsonResult(bookmarkedMovies);
    // }
}