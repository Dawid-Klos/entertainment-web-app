using Entertainment_web_app.Models.Content;

namespace Entertainment_web_app.Services;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAll();
    Task<PagedResponse<MovieDto>> GetAllPaginated(int pageNumber, int pageSize);
    Task<MovieDto> GetById(int movieId);
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(int movieId);
}
