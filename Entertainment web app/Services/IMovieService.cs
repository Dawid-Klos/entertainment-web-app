using Entertainment_web_app.Models.Content;

namespace Entertainment_web_app.Services;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAll();
    Task<PagedResponse<MovieDto>> GetAllPaginated(int pageNumber, int pageSize);
    Task<MovieDto> GetById(int movieId);
    Task Add(Movie movie);
    Task Update(Movie movie);
    Task Delete(int movieId);
}
