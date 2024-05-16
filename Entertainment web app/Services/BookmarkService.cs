using Entertainment_web_app.Repositories;
using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;

public class BookmarkService : IBookmarkService
{

    private readonly IBookmarkRepository _bookmarkRepository;

    public BookmarkService(IBookmarkRepository bookmarkRepository)
    {
        _bookmarkRepository = bookmarkRepository;
    }

    public async Task<Result<PagedResponse<Bookmark>>> GetAll(int pageNumber, int pageSize)
    {
        if (pageSize < 1 || pageSize > 20)
        {
            return Result<PagedResponse<Bookmark>>.Failure(new Error("InvalidPageSize", "The page size is out of range"));
        }

        var totalBookmarks = await _bookmarkRepository.CountAll();
        var totalPages = (int)Math.Ceiling(totalBookmarks / (double)pageSize);

        if (pageNumber < 1 || pageNumber > totalPages)
        {
            return Result<PagedResponse<Bookmark>>.Failure(new Error("InvalidPageNumber", "The page number is out of range"));
        }

        var bookmarks = await _bookmarkRepository.GetAll();

        if (bookmarks == null)
        {
            return Result<PagedResponse<Bookmark>>.Failure(new Error("NotFound", "No bookmarks found"));
        }

        var pagedResponse = new PagedResponse<Bookmark>
        {
            Data = bookmarks,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalRecords = totalBookmarks
        };

        return Result<PagedResponse<Bookmark>>.Success(pagedResponse);
    }

    public async Task<Result<Bookmark>> GetById(string userId, int movieId)
    {
        var bookmark = await _bookmarkRepository.GetById(userId, movieId);

        if (bookmark == null)
        {
            return Result<Bookmark>.Failure(new Error("NotFound", $"Content with ID = {movieId} is not bookmarked"));
        }

        return Result<Bookmark>.Success(bookmark);
    }

    public async Task<Result> Add(Bookmark bookmark)
    {
        var existingBookmark = await _bookmarkRepository.GetById(bookmark.UserId, bookmark.MovieId);

        if (existingBookmark != null)
        {
            return Result.Failure(new Error("AlreadyExists", "Content is already bookmarked"));
        }

        await _bookmarkRepository.Add(bookmark);

        return Result.Success();
    }

    public async Task<Result> Delete(Bookmark bookmark)
    {
        var existingBookmark = await _bookmarkRepository.GetById(bookmark.UserId, bookmark.MovieId);

        if (existingBookmark == null)
        {
            return Result.Failure(new Error("NotFound", "Bookmark not found"));
        }

        await _bookmarkRepository.Delete(bookmark);

        return Result.Success();
    }
}
