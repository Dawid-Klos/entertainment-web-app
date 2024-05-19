using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public interface IBookmarkRepository
{
    Task<IEnumerable<Bookmark>> GetAll();
    Task<IEnumerable<Bookmark>> GetAllPaginated(int pageNumber, int pageSize);
    Task<Bookmark?> GetById(string userId, int movieId);
    Task Add(Bookmark bookmark);
    Task Delete(Bookmark bookmark);
    Task<int> CountAll();
    Task<int> CountByUserId(string userId);
}
