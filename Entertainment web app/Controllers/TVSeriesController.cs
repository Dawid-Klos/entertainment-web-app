using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Services;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Route("api/tv-series")]
[Produces("application/json")]
public class TVSeriesController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly MediaCategory _category = MediaCategory.TVSeries;

    public TVSeriesController(NetwixDbContext context, IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PagedResponse<MovieDto>> Get([FromQuery] PaginationQuery query)
    {
        try
        {
            var tvSeries = await _movieService.GetByCategory(_category, query);

            if (tvSeries.IsFailure)
            {
                return new PagedResponse<MovieDto>
                {
                    Status = "error",
                    Error = tvSeries.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var data = tvSeries.Data!;

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
            return new PagedResponse<MovieDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }


    [HttpGet("search")]
    [Authorize(Roles = "User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<MovieDto>> Get([FromQuery] SearchQuery query)
    {
        try
        {
            var movies = await _movieService.Search(_category, query);

            if (movies.IsFailure)
            {
                return new Response<MovieDto>
                {
                    Status = "error",
                    Error = movies.Error,
                    StatusCode = movies.Error.Code switch
                    {
                        "NotFound" => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status400BadRequest,
                    }
                };
            }

            return new Response<MovieDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = movies.Data
            };
        }
        catch (Exception)
        {
            return new Response<MovieDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }

    [HttpGet("{tvSeriesId}")]
    [Authorize(Roles = "User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MovieDto>> Get(int tvSeriesId)
    {
        try
        {
            var tvSeries = await _movieService.GetById(_category, tvSeriesId);

            if (tvSeries.IsFailure)
            {
                return new JsonResult(new Response<MovieDto>
                {
                    Status = "error",
                    Error = tvSeries.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }

            var data = tvSeries.Data!;

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

    [HttpDelete("{tvSeriesId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<JsonResult> Delete(int tvSeriesId)
    {
        try
        {
            var tvSeries = await _movieService.GetById(_category, tvSeriesId);

            if (tvSeries.IsFailure)
            {
                return new JsonResult(new Response<MovieDto>
                {
                    Status = "error",
                    Error = tvSeries.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }

            // TODO: Implement soft delete instead of hard delete
            // _movieService.Delete(tvSeriesId);

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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<JsonResult> Post([FromBody] Movie newMovie)
    {
        try
        {
            var tvSeriesResult = await _movieService.Add(newMovie);

            if (tvSeriesResult.IsFailure)
            {
                return new JsonResult(new Response<Movie>
                {
                    Status = "error",
                    Error = tvSeriesResult.Error,
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<JsonResult> Put([FromBody] Movie updatedTvSeries)
    {
        try
        {
            var tvSeriesResult = await _movieService.Update(updatedTvSeries);

            if (tvSeriesResult.IsFailure)
            {
                return new JsonResult(new Response<Movie>
                {
                    Status = "error",
                    Error = tvSeriesResult.Error,
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
