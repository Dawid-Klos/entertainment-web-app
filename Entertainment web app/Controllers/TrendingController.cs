using System.Security.Claims;
using Entertainment_web_app.Data;
using Entertainment_web_app.Models.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TrendingController : ControllerBase
{
    
    private readonly NetwixDbContext _context;

    public TrendingController(NetwixDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try {
            var user = await _context.Users.FindAsync(userId); 

            var trendingMovieIds = await _context.Trending
                .Select(t => t.MovieId)
                .ToListAsync();
            
            var movies = await _context.Movies
                .Where(m => trendingMovieIds.Contains(m.MovieId))
                .ToListAsync();

            var bookmarks = await _context.Bookmarks
                .Where(b => b.ApplicationUserId == userId)
                .Select(b => b.MovieId)
                .ToListAsync();

            var trendingMovies = movies.Select(m => new MovieDto 
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    Year = m.Year,
                    Category = m.Category,
                    Rating = m.Rating,
                    ImgSmall = m.ImgSmall,
                    ImgMedium = m.ImgMedium,
                    ImgLarge = m.ImgLarge,
                    IsBookmarked = bookmarks.Contains(m.MovieId) 
                }).ToList();
            
            return new JsonResult(trendingMovies); 
        } catch (Exception ex)
        {
             return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    } 
}
