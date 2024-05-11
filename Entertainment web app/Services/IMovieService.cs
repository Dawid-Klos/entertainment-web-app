using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Models.Responses;

namespace Entertainment_web_app.Services;

public interface IMovieService
{
    Task<Result<IEnumerable<MovieDto>>> GetAll(string category);
    Task<Result<PagedResponse<MovieDto>>> GetAllPaginated(string category, int pageNumber, int pageSize);
    Task<Result<MovieDto>> GetById(string category, int movieId);
    Task<Result> Add(string category, Movie movie);
    Task<Result> Update(string category, Movie movie);
    Task<Result> Delete(int movieId);
}
