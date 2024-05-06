using System.Security.Claims;
using Entertainment_web_app.Data;
using Entertainment_web_app.Models.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
[Route("api/bookmarks")]
[Produces("application/json")]
public class BookmarksController : ControllerBase
{
    private readonly NetwixDbContext _context;

    public BookmarksController(NetwixDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        try
        {
            var bookmarks = await _context.Bookmarks.Where(b => b.UserId == userId).ToListAsync();

            if (bookmarks.Count <= 0)
            {
                return new JsonResult(new { status = "success", statusCode = StatusCodes.Status204NoContent, error = "No bookmarks found." });
            }

            var bookmarkedMovies = bookmarks.Select(b => b.MovieId).ToList();

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = bookmarkedMovies });

        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
        }
    }

    [HttpGet("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        try
        {
            var bookmark = await _context.Bookmarks
                .Where(b => b.UserId == userId && b.MovieId == movieId)
                .FirstOrDefaultAsync();

            if (bookmark == null)
            {
                return new JsonResult(new { status = "error", error = "Bookmark does not exist.", statusCode = StatusCodes.Status404NotFound });
            }

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = bookmark.MovieId });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
        }
    }

    [HttpPost("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        try
        {
            var movie = await _context.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return new JsonResult(new { status = "error", error = "Movie cannot be bookmarked because does not exist.", statusCode = StatusCodes.Status404NotFound });
            }

            var bookmark = await _context.Bookmarks
                .Where(b => b.UserId == userId && b.MovieId == movieId)
                .FirstOrDefaultAsync();

            if (bookmark != null)
            {
                return new JsonResult(new { status = "error", error = "Movie is already bookmarked.", statusCode = StatusCodes.Status400BadRequest });
            }

            var newBookmark = new Bookmark
            {
                UserId = userId,
                MovieId = movieId
            };

            _context.Bookmarks.Add(newBookmark);
            await _context.SaveChangesAsync();

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movie.MovieId });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex.Message}", statusCode = StatusCodes.Status500InternalServerError });
        }
    }

    [HttpDelete("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);

        try
        {
            var bookmark = await _context.Bookmarks
                .Where(b => b.UserId == userId && b.MovieId == movieId)
                .FirstOrDefaultAsync();

            if (bookmark == null)
            {
                return new JsonResult(new { status = "error", error = "Bookmark does not exist.", statusCode = StatusCodes.Status404NotFound });
            }

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();

            return new JsonResult(new { status = "success", statusCode = StatusCodes.Status200OK, data = movieId });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex}", statusCode = StatusCodes.Status500InternalServerError });
        }
    }
}
