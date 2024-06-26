using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public interface ITrendingRepository
{
    Task<IEnumerable<Trending>> GetAll();
    Task<Trending?> GetById(int trendingId);
    Task<Trending?> GetByMovieId(int movieId);
    Task Add(Trending trending);
    Task Update(Trending trending);
    Task Delete(int trendingId);
}

