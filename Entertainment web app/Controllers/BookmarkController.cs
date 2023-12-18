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
public class BookmarkController : ControllerBase
{
    private readonly NetwixDbContext _context;

    public BookmarkController(NetwixDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetBookmarks()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(userId);
        
        if (user == null)
        {
            return NotFound("User not found.");
        }
        
        var bookmarks = await _context.Bookmarks.Where(b => b.ApplicationUserId == userId).ToListAsync();

        if (bookmarks.Count <= 0)
        {
            return NoContent();
        }
        
        var bookmarkedContent = new List<Movie>();
        
        foreach (var bookmark in bookmarks)
        {
            var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.MovieId == bookmark.MovieId);
            
            if (movie != null)
            {
                bookmarkedContent.Add(movie);
            }
        }
        
        return new JsonResult(bookmarkedContent);
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Add([FromQuery] int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _context.Users.FindAsync(userId);
        
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.MovieId == movieId);
        
        if (movie == null)
        {
            return NotFound("Movie not found.");
        }
        
        var newBookmark = new Bookmark {
            ApplicationUserId = userId,
            MovieId = movieId
        };

        try {
            _context.Bookmarks.Add(newBookmark);
            await _context.SaveChangesAsync();  
        } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest("Bookmark already exists.");
        }
        
        return new JsonResult(movie.Title + " added to bookmarks.");
    }
    
    [HttpDelete]
    [Route("[action]")]
    public async Task<IActionResult> Remove([FromQuery] int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _context.Users.FindAsync(userId);
        
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.MovieId == movieId);
        
        if (movie == null)
        {
            return NotFound("Movie not found.");
        }
        
        var bookmark = await _context.Bookmarks.FirstOrDefaultAsync(b => b.ApplicationUserId == userId && b.MovieId == movieId);

        if (bookmark == null)
        {
            return NotFound("Bookmark not found.");
        }

        try {
            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();  
        } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest("Bookmark could not be removed.");
        }
        
        return new JsonResult(movie.Title + " removed from bookmarks.");
    }
}