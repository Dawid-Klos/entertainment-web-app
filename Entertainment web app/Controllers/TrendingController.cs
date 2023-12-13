using Entertainment_web_app.Data;
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
    public async Task<IActionResult> GetTrending()
    {
        string sqlQuery = "SELECT * FROM \"Trending\"";
        
        try {
            var trendingContent = await _context.Trending
                .FromSqlRaw(sqlQuery).ToListAsync();

            foreach (var trending in trendingContent)
            {
                var movie = await _context.Movies.FindAsync(trending.MovieId);
                
                if (movie == null)
                {
                    return NotFound();
                }
                
                trending.Movie = movie; 
            }
            
            
            return new JsonResult(trendingContent);
            
        } catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
    
    
    
}