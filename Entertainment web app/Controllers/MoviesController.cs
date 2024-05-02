using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Entertainment_web_app.Data;
using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Services;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[Produces("application/json")]
public class MoviesController : ControllerBase
{
    private readonly NetwixDbContext _context;
    private readonly IMovieService _movieService;

    public MoviesController(NetwixDbContext context, IMovieService movieService)
    {
        _context = context;
        _movieService = movieService;
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

            var movies = await _movieService.GetAll();

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movies });
        }
        catch (Exception)
        {
            return new JsonResult(new { status = "error", error = "Internal Server Error", statusCode = StatusCodes.Status500InternalServerError });
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

        try
        {
            var user = await _context.Users.FindAsync(userId);

            var bookmarks = await _context.Bookmarks
                .Where(b => b.UserId == userId)
                .Select(b => b.MovieId)
                .ToListAsync();

            var movie = await _movieService.GetById(movieId);

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movie });
        }
        catch (ArgumentException ex)
        {
            return new JsonResult(new { status = "error", error = ex.Message, statusCode = StatusCodes.Status404NotFound });
        }
        catch (Exception)
        {
            return new JsonResult(new { status = "error", error = "Internal Server Error", statusCode = StatusCodes.Status500InternalServerError });
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

            // TODO: Implement soft delete instead of hard delete
            // _movieService.Delete(movieId);

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK });
        }
        catch (ArgumentException ex)
        {
            return new JsonResult(new { status = "error", error = ex.Message, statusCode = StatusCodes.Status404NotFound });
        }
        catch (Exception)
        {
            return new JsonResult(new { status = "error", error = "Internal Server Error", statusCode = StatusCodes.Status500InternalServerError });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<JsonResult> Post([FromBody] Movie newMovie)
    {
        try
        {
            await _movieService.Add(newMovie);

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK });
        }
        catch (ArgumentException ex)
        {
            return new JsonResult(new { status = "error", error = ex.Message, statusCode = StatusCodes.Status400BadRequest });
        }
        catch (Exception)
        {
            return new JsonResult(new { status = "error", error = "Internal Server Error", statusCode = StatusCodes.Status500InternalServerError });
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<JsonResult> Put([FromBody] Movie updatedMovie)
    {
        try
        {
            await _movieService.Update(updatedMovie);

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK });
        }
        catch (ArgumentException ex)
        {
            return new JsonResult(new { status = "error", error = ex.Message, statusCode = StatusCodes.Status404NotFound });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new JsonResult(new { status = "error", error = "Internal Server Error", statusCode = StatusCodes.Status500InternalServerError });
        }
    }
}
