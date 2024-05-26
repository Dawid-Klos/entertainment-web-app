using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;

public interface IBookmarkService
{
    Task<Result<PagedResponse<Bookmark>>> GetAll(int pageNumber, int pageSize);
    Task<Result<IEnumerable<Bookmark>>> GetByUser(MediaCategory category, string userId);
    Task<Result<Bookmark>> GetById(string userId, int movieId);
    Task<Result> Add(Bookmark bookmark);
    Task<Result> Delete(Bookmark bookmark);
}
