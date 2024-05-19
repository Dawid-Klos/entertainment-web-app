using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Entertainment_web_app.Services;
using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
[Route("api/trending")]
[Produces("application/json")]
public class TrendingController : ControllerBase
{
    private readonly ITrendingService _trendingService;

    public TrendingController(ITrendingService trendingService)
    {
        _trendingService = trendingService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<TrendingDto>> Get()
    {
        try
        {
            var trendingMovies = await _trendingService.GetAll();

            if (trendingMovies.IsFailure)
            {
                return new Response<TrendingDto>
                {
                    Status = "error",
                    Error = trendingMovies.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            return new Response<TrendingDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = trendingMovies.Data!,
            };

        }
        catch (Exception)
        {
            return new Response<TrendingDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }

    [HttpGet("{trendingId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<TrendingDto>> Get(int trendingId)
    {

        try
        {
            var trendingMovie = await _trendingService.GetById(trendingId);

            if (trendingMovie.IsFailure)
            {
                return new Response<TrendingDto>
                {
                    Status = "error",
                    Error = trendingMovie.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new Response<TrendingDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = new List<TrendingDto> { trendingMovie.Data! },
            };

        }
        catch (Exception)
        {
            return new Response<TrendingDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<TrendingDto>> Post([FromBody] Trending trending)
    {
        try
        {
            var trendingMovie = await _trendingService.Add(trending);

            if (trendingMovie.IsFailure)
            {
                return new Response<TrendingDto>
                {
                    Status = "error",
                    Error = trendingMovie.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            return new Response<TrendingDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status201Created,
            };
        }
        catch (Exception)
        {
            return new Response<TrendingDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }

    [HttpDelete("{trendingId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<TrendingDto>> Delete(int trendingId)
    {
        try
        {
            var trendingMovie = await _trendingService.Delete(trendingId);

            if (trendingMovie.IsFailure)
            {
                return new Response<TrendingDto>
                {
                    Status = "error",
                    Error = trendingMovie.Error,
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new Response<TrendingDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception)
        {
            return new Response<TrendingDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
