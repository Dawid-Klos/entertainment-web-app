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
[Produces("application/json")]
public class TrendingController : ControllerBase
{

    private readonly NetwixDbContext _context;

    public TrendingController(NetwixDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var user = await _context.Users.FindAsync(userId);

            var trendingMovieIds = await _context.Trending
                .Select(t => t.MovieId)
                .ToListAsync();

            var movies = await _context.Movies
                .Where(m => trendingMovieIds.Contains(m.MovieId))
                .ToListAsync();

            var bookmarks = await _context.Bookmarks
                .Where(b => b.UserId == userId)
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

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = trendingMovies });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
        }
    }

    [HttpGet("{trendingId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MovieDto>> Get(int trendingId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        try
        {
            var trendingMovie = await _context.Trending.FindAsync(trendingId);

            if (trendingMovie == null)
            {
                return new JsonResult(new { status = "error", error = $"Trending movie with id = {trendingId} does not exist in the database", statusCode = StatusCodes.Status404NotFound });
            }

            var movie = await _context.Movies
                .Where(m => m.MovieId == trendingMovie.MovieId)
                .FirstOrDefaultAsync();

            var bookmark = await _context.Bookmarks
                .Where(b => b.UserId == userId && b.MovieId == movie!.MovieId)
                .FirstOrDefaultAsync();

            var movieDto = new MovieDto
            {
                MovieId = movie!.MovieId,
                Title = movie!.Title,
                Year = movie!.Year,
                Category = movie!.Category,
                Rating = movie!.Rating,
                ImgSmall = movie!.ImgSmall,
                ImgMedium = movie!.ImgMedium,
                ImgLarge = movie!.ImgLarge,
                IsBookmarked = bookmark != null
            };

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movieDto });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
        }
    }
}
