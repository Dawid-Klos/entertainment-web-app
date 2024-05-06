using Entertainment_web_app.Models.Content;

namespace Entertainment_web_app.Repositories;

public interface IBookmarkRepository
{
    Task<IEnumerable<Bookmark>> GetAll();
    Task<Bookmark> GetById(int id);
    void Add(Bookmark bookmark);
    void Update(Bookmark bookmark);
    void Delete(int id);
}
