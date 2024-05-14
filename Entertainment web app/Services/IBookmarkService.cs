using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Common.Responses;

namespace Entertainment_web_app.Services;

public interface IBookmarkService
{
    Task<Result<PagedResponse<Bookmark>>> GetAll(int pageNumber, int pageSize);
    Task<Result<Bookmark>> GetById(string userId, int movieId);
    Task<Result<Bookmark>> Add(Bookmark bookmark);
    Task<Result<Bookmark>> Update(Bookmark bookmark);
    Task<Result<Bookmark>> Delete(Bookmark bookmark);
}
