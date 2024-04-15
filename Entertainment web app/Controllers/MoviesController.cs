using System.Security.Claims;
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
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);


        try
        {
            var movies = await _context.Movies
                .Select(m => new MovieDto
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    Year = m.Year,
                    Category = m.Category,
                    Rating = m.Rating,
                    ImgSmall = m.ImgSmall,
                    ImgMedium = m.ImgMedium,
                    ImgLarge = m.ImgLarge,
                    IsBookmarked = false 
                }).ToListAsync();

            var bookmarks = await _context.Bookmarks.Where(b => b.ApplicationUserId == userId).Select(b => b.MovieId).ToListAsync();


            foreach (var movie in movies)
            {
                movie.IsBookmarked = bookmarks.Contains(movie.MovieId);
            }

            return new JsonResult(movies);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTrending()
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
}
