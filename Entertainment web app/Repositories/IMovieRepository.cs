using Entertainment_web_app.Models.Content;

namespace Entertainment_web_app.Repositories;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAll();
    Task<IEnumerable<Movie>> GetAllPaginated(int pageNumber, int pageSize);
    Task<IEnumerable<Movie>> GetByCategoryPaginated(string category, int pageNumber, int pageSize);
    Task<Movie> GetById(int movieId);
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(int movieId);
    Task<int> CountAll();
    Task<int> CountByCategory(string category);
}
