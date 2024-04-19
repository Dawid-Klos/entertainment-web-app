using Entertainment_web_app.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly NetwixDbContext _context;

    public SearchController(NetwixDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> SearchByTitle([FromQuery] string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return BadRequest("Search query cannot be empty.");
        }

        var titleParam = new NpgsqlParameter(":title", "%" + title + "%");

        string sqlQuery = "SELECT * FROM \"Movies\" WHERE \"Title\" LIKE :title";

        try
        {
            var searchResult = await _context.Movies.FromSqlRaw(sqlQuery, titleParam).ToListAsync();
            return new JsonResult(searchResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> SearchByCategory([FromQuery] string category)
    {
        if (string.IsNullOrEmpty(category))
        {
            return BadRequest("Search category cannot be empty.");
        }

        var categoryParam = new NpgsqlParameter(":category", "%" + category + "%");
        string sqlQuery = "SELECT * FROM \"Movies\" WHERE \"Category\" LIKE :category";

        try
        {
            var searchResult = await _context.Movies
                .FromSqlRaw(sqlQuery, categoryParam).ToListAsync();

            return new JsonResult(searchResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> SearchByCategoryAndTitle([FromQuery] string category, [FromQuery] string title)
    {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(category))
        {
            return BadRequest("Search title and category cannot be empty.");
        }

        var titleParam = new NpgsqlParameter(":title", "%" + title + "%");
        var categoryParam = new NpgsqlParameter(":category", "%" + category + "%");

        string sqlQuery = "SELECT * FROM \"Movies\" WHERE \"Title\" LIKE :title AND \"Category\" LIKE :category";

        try
        {
            var searchResult = await _context.Movies
                .FromSqlRaw(sqlQuery, titleParam, categoryParam).ToListAsync();

            return new JsonResult(searchResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
}
