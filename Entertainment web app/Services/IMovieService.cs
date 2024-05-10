using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Models.Responses;

namespace Entertainment_web_app.Services;

public interface IMovieService
{
    Task<Result<IEnumerable<MovieDto>>> GetAll();
    Task<Result<PagedResponse<MovieDto>>> GetAllPaginated(int pageNumber, int pageSize);
    Task<Result<MovieDto>> GetById(int movieId);
    Task<Result> Add(Movie movie);
    Task<Result> Update(Movie movie);
    Task<Result> Delete(int movieId);
}
