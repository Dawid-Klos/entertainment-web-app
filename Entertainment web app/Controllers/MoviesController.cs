using Entertainment_web_app.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly NetwixDbContext _context;

    public MoviesController(NetwixDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _context.Movies.FromSqlRaw("SELECT * FROM Movies WHERE Category= 'Movie'").ToListAsync();
        return new JsonResult(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _context.Movies
            .FromSqlRaw("SELECT * FROM Movies WHERE MovieId = '{0}'", string.Format("%{0}%", id)).FirstAsync();
        ;
        return new JsonResult(movie);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTrendingMovies(bool isTrending)
    {
        var trendingMovies =
            await _context.Movies.FromSqlRaw("SELECT * FROM Movies WHERE IsTrending = 1").ToListAsync();
        return new JsonResult(trendingMovies);
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetTvSeries()
    {
        var tvSeries =
            await _context.Movies.FromSqlRaw("SELECT * FROM Movies WHERE Category = 'Tv Series'").ToListAsync();
        return new JsonResult(tvSeries);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetBookmarkedMovies(string userId)
    {
        // var user = await _context.Users.FindAsync(userId);
        var user = await _context.Users.FindAsync(userId);
        var bookmarkedMovies = _context.Movies.FromSqlRaw(
            "SELECT * FROM Movies INNER JOIN ApplicationUserMovie ON ApplicationUserMovie.MoviesMovieId = AspNetUsers.Id WHERE isBookmarked = 1").ToListAsync();

        return new JsonResult(bookmarkedMovies);
    }
}