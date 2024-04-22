using Entertainment_web_app.Models.Content;

namespace Entertainment_web_app.Services;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAll();
    PagedResponse<Movie> GetAllPaginated(int pageNumber, int pageSize);
    PagedResponse<Movie> GetByCategoryPaginated(string category, int pageNumber, int pageSize);
    Task<Movie> GetById(int movieId);
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(int movieId);
}
