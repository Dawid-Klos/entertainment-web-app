using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Common.Responses;

namespace Entertainment_web_app.Services;

public interface IMovieService
{
    Task<Result<IEnumerable<MovieDto>>> GetAll(MediaCategory category);
    Task<Result<PagedResponse<MovieDto>>> GetAllPaginated(MediaCategory category, int pageNumber, int pageSize);
    Task<Result<MovieDto>> GetById(MediaCategory category, int movieId);
    Task<Result> Add(Movie movie);
    Task<Result> Update(Movie movie);
    Task<Result> Delete(int movieId);
}
