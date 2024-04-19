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
public class TVSeriesController : ControllerBase
{
    private readonly NetwixDbContext _context;

    public TVSeriesController(NetwixDbContext context)
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
                .Where(b => b.UserId == userId)
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
                    IsBookmarked = bookmarks!.Contains(m.MovieId)
                }).ToListAsync();


            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movies });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
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
                .Where(b => b.UserId == userId)
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
                return new JsonResult(new { status = "error", error = "Movie with this Id does not exist in the database", statusCode = StatusCodes.Status404NotFound });
            }

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movie });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
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
                return new JsonResult(new { status = "error", error = "Movie with this Id does not exist in the database", statusCode = StatusCodes.Status404NotFound });
            }
            // TODO: Implement soft delete instead of hard delete
            // _context.Movies.Remove(movie);
            // await _context.SaveChangesAsync();
            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movie.MovieId });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
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

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movie });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
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
                return new JsonResult(new { status = "error", error = "Movie with this Id cannot be updated because does not exist in the database", statusCode = StatusCodes.Status404NotFound });
            }

            movie.Title = updatedMovie.Title;
            movie.Year = updatedMovie.Year;
            movie.Category = updatedMovie.Category;
            movie.Rating = updatedMovie.Rating;
            movie.ImgSmall = updatedMovie.ImgSmall;
            movie.ImgMedium = updatedMovie.ImgMedium;
            movie.ImgLarge = updatedMovie.ImgLarge;

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movie });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
        }
    }
}
