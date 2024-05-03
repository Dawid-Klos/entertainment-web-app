using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Services;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[Produces("application/json")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResponse<MovieDto>>> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var movies = await _movieService.GetAllPaginated(pageNumber, pageSize);

            var response = new PagedResponse<MovieDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                PageNumber = movies.PageNumber,
                PageSize = movies.PageSize,
                TotalPages = movies.TotalPages,
                TotalRecords = movies.TotalRecords,
                Data = movies.Data,
            };

            return new JsonResult(response);
        }
        catch (ArgumentException ex)
        {
            var response = new PagedResponse<MovieDto>
            {
                Status = "error",
                Error = ex.Message,
                StatusCode = StatusCodes.Status400BadRequest,
            };

            return new JsonResult(response);
        }
        catch (Exception)
        {
            var response = new PagedResponse<MovieDto>
            {
                Status = "error",
                Error = "Internal Server Error",
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            return new JsonResult(response);
        }
    }

    [HttpGet("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MovieDto>> Get(int movieId)
    {
        try
        {
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
    public async Task<JsonResult> Delete(int movieId)
    {
        try
        {
            var movie = await _movieService.GetById(movieId);

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
