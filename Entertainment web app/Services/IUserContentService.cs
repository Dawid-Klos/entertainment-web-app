using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Dto;

namespace Entertainment_web_app.Services;

public interface IUserContentService
{
    Task<Result<PagedResponse<MovieDto>>> GetMovies(string UserId, PaginationQuery query);
    Task<Result<PagedResponse<MovieDto>>> GetTvSeries(string userId, PaginationQuery query);
}
