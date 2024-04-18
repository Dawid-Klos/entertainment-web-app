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
public class MoviesController : ControllerBase
{
    private readonly NetwixDbContext _context;

    public MoviesController(NetwixDbContext context)
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

            var bookmarks = await _context.Bookmarks
                .Where(b => b.ApplicationUserId == userId)
                .Select(b => b.MovieId)
                .ToListAsync();

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
                    IsBookmarked = bookmarks.Contains(m.MovieId)
                }).ToListAsync();

            return new JsonResult(movies);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MovieDto>> Get(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        try
        {

            var bookmarks = await _context.Bookmarks
                .Where(b => b.ApplicationUserId == userId)
                .Select(b => b.MovieId)
                .ToListAsync();

            var movie = await _context.Movies
                .Where(m => m.MovieId == movieId)
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
                    IsBookmarked = bookmarks.Contains(m.MovieId)
                }).FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound(new JsonResult(movieId));
            }

            return new JsonResult(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int movieId)
    {
        try
        {
            var movie = await _context.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return NotFound(movieId);
            }
            // TODO: Implement soft delete instead of hard delete
            // _context.Movies.Remove(movie);
            // await _context.SaveChangesAsync();
            return Ok(movieId);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] Movie newMovie)
    {
        try
        {
            var movie = new Movie
            {
                Title = newMovie.Title,
                Year = newMovie.Year,
                Category = newMovie.Category,
                Rating = newMovie.Rating,
                ImgSmall = newMovie.ImgSmall,
                ImgMedium = newMovie.ImgMedium,
                ImgLarge = newMovie.ImgLarge
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }



    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] Movie updatedMovie)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        try
        {
            var movie = await _context.Movies.FindAsync(updatedMovie.MovieId);

            if (movie == null)
            {
                return NotFound(updatedMovie.MovieId);
            }

            movie.Title = movie.Title;
            movie.Year = movie.Year;
            movie.Category = movie.Category;
            movie.Rating = movie.Rating;
            movie.ImgSmall = movie.ImgSmall;
            movie.ImgMedium = movie.ImgMedium;
            movie.ImgLarge = movie.ImgLarge;

            await _context.SaveChangesAsync();

            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
}
