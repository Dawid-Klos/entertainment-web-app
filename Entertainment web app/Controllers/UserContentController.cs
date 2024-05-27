using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Services;

namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize]
[Route("api/user-content")]
[Produces("application/json")]
public class UserContentController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserContentService _userContentService;

    public UserContentController(IUserService userService, IUserContentService userContentService)
    {
        _userService = userService;
        _userContentService = userContentService;
    }

    [HttpGet("movies")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PagedResponse<MovieDto>> GetMovies([FromQuery] PaginationQuery query)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userContentService.GetMovies(userId, query);

            if (result.IsFailure)
            {
                return new PagedResponse<MovieDto>
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status404NotFound,
                    Error = result.Error
                };
            }

            return new PagedResponse<MovieDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = result.Data!.Data,
                TotalRecords = result.Data!.TotalRecords,
                PageNumber = result.Data!.PageNumber,
                PageSize = result.Data!.PageSize,
                TotalPages = result.Data!.TotalPages,
            };
        }
        catch (Exception)
        {
            return new PagedResponse<MovieDto>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("Internal Server Error", "An error occurred while processing the request")
            };
        }
    }

    [HttpGet("movies/search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<MovieDto>> Search([FromQuery] SearchQuery query)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userContentService.Search(userId, MediaCategory.Movies, query);

            if (result.IsFailure)
            {
                return new Response<MovieDto>
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status404NotFound,
                    Error = result.Error
                };
            }

            return new Response<MovieDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = result.Data!,
            };
        }
        catch (Exception)
        {
            return new Response<MovieDto>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }

    [HttpGet("tv-series")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PagedResponse<MovieDto>> GetTvSeries([FromQuery] PaginationQuery query)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userContentService.GetTvSeries(userId, query);

            if (result.IsFailure)
            {
                return new PagedResponse<MovieDto>
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status404NotFound,
                    Error = result.Error
                };
            }

            return new PagedResponse<MovieDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = result.Data!.Data
            };
        }
        catch (Exception)
        {
            return new PagedResponse<MovieDto>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }


    [HttpGet("tv-series/search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<MovieDto>> SearchTvSeries([FromQuery] SearchQuery query)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userContentService.Search(userId, MediaCategory.TVSeries, query);

            if (result.IsFailure)
            {
                return new Response<MovieDto>
                {
                    Status = "error",
                    StatusCode = StatusCodes.Status404NotFound,
                    Error = result.Error
                };
            }

            return new Response<MovieDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = result.Data!,
            };
        }
        catch (Exception)
        {
            return new Response<MovieDto>
            {
                Status = "error",
                StatusCode = StatusCodes.Status500InternalServerError,
                Error = new Error("InternalServerError", "An error occurred while processing the request")
            };
        }
    }
}
