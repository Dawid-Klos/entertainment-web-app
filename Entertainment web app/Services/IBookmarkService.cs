using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;

public interface IBookmarkService
{
    Task<Result<PagedResponse<Bookmark>>> GetAll(int pageNumber, int pageSize);
    Task<Result<Bookmark>> GetById(string userId, int movieId);
    Task<Result> Add(Bookmark bookmark);
    Task<Result> Delete(Bookmark bookmark);
}
