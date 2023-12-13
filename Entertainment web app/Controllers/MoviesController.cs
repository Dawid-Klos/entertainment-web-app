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
    public async Task<IActionResult> GetMovies()
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