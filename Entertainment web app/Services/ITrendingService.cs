using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;

public interface ITrendingService
{
    Task<Result<PagedResponse<MovieDto>>> GetAll(int pageNumber, int pageSize);
    Task<Result<MovieDto>> GetById(int trendingId);
    Task<Result> Add(Trending trending);
    Task<Result> Update(Trending trending);
    Task<Result> Delete(int trendingId);
}
