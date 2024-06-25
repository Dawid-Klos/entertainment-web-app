using System.Security.Claims;

using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Repositories;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;


public class TrendingService : ITrendingService
{
    private readonly ITrendingRepository _trendingRepository;
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TrendingService(ITrendingRepository trendingRepository, IBookmarkRepository bookmarkRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _trendingRepository = trendingRepository;
        _bookmarkRepository = bookmarkRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    private async Task<List<int>?> GetBookmarkedMoviesIds()
    {
        var user = _httpContextAccessor.HttpContext!.User;
        if (user.IsInRole("Admin")) { return null; }

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        var userBookmarks = await _bookmarkRepository.GetByUserId(userId);
        if (userBookmarks == null || !userBookmarks.Any()) { return null; }

        return userBookmarks.Select(b => b.MovieId).ToList();
    }

    public async Task<Result<IEnumerable<TrendingDto>>> GetAll()
    {

        var trendingMovies = await _trendingRepository.GetAll();

        if (trendingMovies == null || !trendingMovies.Any())
        {
            return Result<IEnumerable<TrendingDto>>.Failure(new Error("NotFound", "No trending movies found"));
        }

        var bookmarkedMoviesIds = await GetBookmarkedMoviesIds();

        var trendingDtos = trendingMovies.Select(m => new TrendingDto
        {
            MovieId = m.Movie.MovieId,
            Title = m.Movie.Title,
            Year = m.Movie.Year,
            Category = m.Movie.Category,
            Rating = m.Movie.Rating,
            ImgSmall = m.ImgTrendingSmall,
            ImgLarge = m.ImgTrendingLarge,
            IsBookmarked = bookmarkedMoviesIds?.Contains(m.Movie.MovieId) ?? false
        }).ToList();

        return Result<IEnumerable<TrendingDto>>.Success(trendingDtos);
    }

    public async Task<Result<TrendingDto>> GetById(int trendingId)
    {
        var trendingMovie = await _trendingRepository.GetById(trendingId);

        if (trendingMovie == null)
        {
            return Result<TrendingDto>.Failure(new Error("NotFound", $"Trending movie with ID = {trendingId} does not exist"));
        }

        var bookmarkedMoviesIds = await GetBookmarkedMoviesIds();

        var trendingDto = new TrendingDto
        {
            MovieId = trendingMovie.Movie.MovieId,
            Title = trendingMovie.Movie.Title,
            Year = trendingMovie.Movie.Year,
            Category = trendingMovie.Movie.Category,
            Rating = trendingMovie.Movie.Rating,
            ImgSmall = trendingMovie.ImgTrendingSmall,
            ImgLarge = trendingMovie.ImgTrendingLarge,
            IsBookmarked = bookmarkedMoviesIds?.Contains(trendingMovie.Movie.MovieId) ?? false
        };

        return Result<TrendingDto>.Success(trendingDto);
    }

    public async Task<Result> Add(Trending newTrending)
    {
        var trendingExists = await _trendingRepository.GetById(newTrending.TrendingId);
        var movieExists = await _trendingRepository.GetByMovieId(newTrending.MovieId);

        if (trendingExists != null || movieExists != null)
        {
            return Result.Failure(new Error("AlreadyExists", $"Movie {newTrending.Movie.Title} is already trending"));
        }

        await _trendingRepository.Add(newTrending);

        return Result.Success();
    }

    public async Task<Result> Update(Trending trending)
    {
        if (trending == null)
        {
            return Result.Failure(new Error("BadRequest", "Provided trending movie is null"));
        }

        var trendingExists = await _trendingRepository.GetById(trending.TrendingId);

        if (trendingExists == null)
        {
            return Result.Failure(new Error("NotFound", $"Trending movie with ID = {trending.TrendingId} does not exist"));
        }

        await _trendingRepository.Update(trending);

        return Result.Success();
    }

    public async Task<Result> Delete(int trendingId)
    {
        var trendingExists = await _trendingRepository.GetById(trendingId);

        if (trendingExists == null)
        {
            return Result.Failure(new Error("NotFound", $"Trending movie with ID = {trendingId} does not exist"));
        }

        await _trendingRepository.Delete(trendingId);

        return Result.Success();
    }
}
