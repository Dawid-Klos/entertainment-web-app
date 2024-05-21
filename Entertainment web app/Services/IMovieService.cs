using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;

public interface IMovieService
{
    Task<Result<PagedResponse<MovieDto>>> GetAll(PaginationQuery query);
    Task<Result<PagedResponse<MovieDto>>> GetByCategory(MediaCategory category, PaginationQuery query);
    Task<Result<IEnumerable<MovieDto>>> Search(MediaCategory category, SearchQuery query);
    Task<Result<MovieDto>> GetById(MediaCategory category, int movieId);
    Task<Result> Add(Movie movie);
    Task<Result> Update(Movie movie);
    Task<Result> Delete(int movieId);
}
