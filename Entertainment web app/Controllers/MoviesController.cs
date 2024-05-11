using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Models.Responses;
using Entertainment_web_app.Services;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
[Route("api/movies")]
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

            if (movies.IsFailure)
            {
                return new JsonResult(new Response<MovieDto>
                {
                    Status = "error",
                    Error = movies.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                });
            }

            var data = movies.Data!;

            return new PagedResponse<MovieDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                PageNumber = data.PageNumber,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                TotalRecords = data.TotalRecords,
                Data = data.Data,
            };
        }
        catch (Exception)
        {
            var response = new Response<MovieDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
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

            if (movie.IsFailure)
            {
                return new JsonResult(new Response<MovieDto>
                {
                    Status = "error",
                    Error = movie.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }

            var data = movie.Data!;

            var response = new Response<MovieDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = new List<MovieDto> { data },
            };

            return new JsonResult(response);
        }
        catch (Exception)
        {
            var response = new Response<MovieDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            return new JsonResult(response);
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

            if (movie.IsFailure)
            {
                return new JsonResult(new Response<MovieDto>
                {
                    Status = "error",
                    Error = movie.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }

            // TODO: Implement soft delete instead of hard delete
            // _movieService.Delete(movieId);

            var response = new Response<Movie>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };

            return new JsonResult(response);
        }
        catch (Exception)
        {
            var response = new Response<MovieDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            return new JsonResult(response);
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
            var movieResult = await _movieService.Add(newMovie);

            if (movieResult.IsFailure)
            {
                return new JsonResult(new Response<Movie>
                {
                    Status = "error",
                    Error = movieResult.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                });
            }

            var response = new Response<Movie>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = new List<Movie> { newMovie },
            };

            return new JsonResult(response);
        }
        catch (Exception)
        {
            var response = new Response<Movie>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            return new JsonResult(response);
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
            var movieResult = await _movieService.Update(updatedMovie);

            if (movieResult.IsFailure)
            {
                return new JsonResult(new Response<Movie>
                {
                    Status = "error",
                    Error = movieResult.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }

            var response = new Response<Movie>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };

            return new JsonResult(response);
        }
        catch (Exception)
        {
            var response = new Response<Movie>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            return new JsonResult(response);
        }
    }
}
