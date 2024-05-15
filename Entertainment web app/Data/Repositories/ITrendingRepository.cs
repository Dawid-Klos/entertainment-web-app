using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public interface ITrendingRepository
{
    Task<IEnumerable<Trending>> GetAll();
    Task<Trending> GetById(int trendingId);
    void Add(Trending trending);
    void Update(Trending trending);
    void Delete(int trendingId);
}

