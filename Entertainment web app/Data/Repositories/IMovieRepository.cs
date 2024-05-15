using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAll();
    Task<IEnumerable<Movie>> GetAllPaginated(int pageNumber, int pageSize);
    Task<IEnumerable<Movie>> GetByCategory(string category);
    Task<IEnumerable<Movie>> GetByCategoryPaginated(string category, int pageNumber, int pageSize);
    Task<Movie?> GetById(int movieId);
    Task Add(Movie movie);
    Task Update(Movie movie);
    Task Delete(int movieId);
    Task<int> CountAll();
    Task<int> CountByCategory(string category);
}
