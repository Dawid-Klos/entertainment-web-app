using Entertainment_web_app.Models.Content;

namespace Entertainment_web_app.Repositories;

public interface ITrendingRepository
{
    Task<IEnumerable<Trending>> GetAll();
    Trending GetById(int trendingId);
    void Add(Movie trending);
    void Update(Movie trending);
    void Delete(int trendingId);
}

