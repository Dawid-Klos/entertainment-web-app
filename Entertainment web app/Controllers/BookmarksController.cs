using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Data;
using Entertainment_web_app.Services;


namespace Entertainment_web_app.Controllers;

[ApiController]
[Authorize(Roles = "User")]
[Route("api/users/bookmarks")]
[Produces("application/json")]
public class BookmarksController : ControllerBase
{
    private readonly IBookmarkService _bookmarkService;
    private readonly IUserService _userService;

    public BookmarksController(IBookmarkService bookmarkService, IUserService userService)
    {
        _bookmarkService = bookmarkService;
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PagedResponse<BookmarkDto>> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var bookmarks = await _bookmarkService.GetAll(pageNumber, pageSize);

            if (bookmarks.IsFailure)
            {
                return new PagedResponse<BookmarkDto>
                {
                    Status = "error",
                    Error = bookmarks.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var bookmarksData = bookmarks.Data!;

            var bookmarkDtos = bookmarksData.Data!.Select(bookmark => new BookmarkDto
            {
                UserId = bookmark.UserId,
                MovieId = bookmark.MovieId
            }).ToList();

            return new PagedResponse<BookmarkDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                PageNumber = bookmarksData.PageNumber,
                PageSize = bookmarksData.PageSize,
                TotalPages = bookmarksData.TotalPages,
                TotalRecords = bookmarksData.TotalRecords,
                Data = bookmarkDtos
            };
        }
        catch (Exception)
        {
            return new PagedResponse<BookmarkDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", "An error occurred while processing your request."),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }

    [HttpGet("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<BookmarkDto>> Get(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetById(userId);

        try
        {
            var bookmark = await _bookmarkService.GetById(userId, movieId);

            if (bookmark.IsFailure)
            {
                return new Response<BookmarkDto>
                {
                    Status = "error",
                    Error = bookmark.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var userBookmark = bookmark.Data!;

            var bookmarkDto = new BookmarkDto
            {
                UserId = userBookmark.UserId,
                MovieId = userBookmark.MovieId
            };

            return new Response<BookmarkDto>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
                Data = new List<BookmarkDto> { bookmarkDto },
            };
        }
        catch (Exception ex)
        {
            return new Response<BookmarkDto>
            {
                Status = "error",
                Error = new Error("Internal Server Error", $"An error occurred while processing your request, {ex}"),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }

    [HttpPost("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var newBookmark = new Bookmark
            {
                UserId = userId,
                MovieId = movieId
            };

            var addedBookmark = await _bookmarkService.Add(newBookmark);

            if (addedBookmark.IsFailure)
            {
                return new JsonResult(new Response<Bookmark>
                {
                    Status = "error",
                    Error = addedBookmark.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                });
            }

            return new JsonResult(new Response<Bookmark>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { status = "error", error = $"Internal Server Error, {ex.Message}", statusCode = StatusCodes.Status500InternalServerError });
        }
    }

    [HttpDelete("{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<Bookmark>> Delete(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var bookmark = new Bookmark
            {
                UserId = userId,
                MovieId = movieId
            };

            var deletedBookmark = await _bookmarkService.Delete(bookmark);

            if (deletedBookmark.IsFailure)
            {
                return new Response<Bookmark>
                {
                    Status = "error",
                    Error = deletedBookmark.Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            return new Response<Bookmark>
            {
                Status = "success",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        catch (Exception ex)
        {
            return new Response<Bookmark>
            {
                Status = "error",
                Error = new Error("Internal Server Error", $"An error occurred while processing your request, {ex}"),
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
