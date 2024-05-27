using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Repositories;
using Entertainment_web_app.Models.Dto;

namespace Entertainment_web_app.Services;

public class UserContentService : IUserContentService
{

    private readonly IMovieRepository _movieRepository;
    private readonly IBookmarkRepository _bookmarkRepository;

    public UserContentService(IMovieRepository movieRepository, IBookmarkRepository bookmarkRepository)
    {
        _movieRepository = movieRepository;
        _bookmarkRepository = bookmarkRepository;
    }

    public async Task<Result<PagedResponse<MovieDto>>> GetMovies(string userId, PaginationQuery query)
    {
        if (query.PageSize < 1 || query.PageSize > 20)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageSize", "The page size is out of range"));
        }

        if (query.PageNumber < 1)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageNumber", "The page number is out of range"));
        }

        var category = MediaCategory.Movies.ToString();
        var bookmarks = await _bookmarkRepository.GetByCategoryAndUserId(category, userId);

        if (bookmarks == null)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("NotFound", "No bookmarked movies found"));
        }

        var movieIds = bookmarks.Select(b => b.MovieId);
        var bookmarkedMovies = await _movieRepository.GetByIds(movieIds);

        var totalMovies = bookmarkedMovies.Count();
        var totalPages = (int)Math.Ceiling(totalMovies / (double)query.PageSize);

        if (query.PageNumber > totalPages)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageNumber", "The page number is out of range"));
        }

        var movieDtos = bookmarkedMovies.Select(movie => new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Year = movie.Year,
            Category = movie.Category,
            Rating = movie.Rating,
            ImgSmall = movie.ImgSmall,
            ImgMedium = movie.ImgMedium,
            ImgLarge = movie.ImgLarge,
            IsBookmarked = true
        });

        var pagedResponse = new PagedResponse<MovieDto>
        {
            Data = movieDtos,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalPages = totalPages,
            TotalRecords = totalMovies
        };

        return Result<PagedResponse<MovieDto>>.Success(pagedResponse);
    }


    public async Task<Result<PagedResponse<MovieDto>>> GetTvSeries(string userId, PaginationQuery query)
    {
        if (query.PageSize < 1 || query.PageSize > 20)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageSize", "The page size is out of range"));
        }

        if (query.PageNumber < 1)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageNumber", "The page number is out of range"));
        }

        var category = MediaCategory.TVSeries.ToString();
        var bookmarks = await _bookmarkRepository.GetByCategoryAndUserId(category, userId);

        if (bookmarks == null)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("NotFound", "No bookmarked movies found"));
        }

        var movieIds = bookmarks.Select(b => b.MovieId);
        var bookmarkedTVSeries = await _movieRepository.GetByIds(movieIds);

        var totalMovies = bookmarkedTVSeries.Count();
        var totalPages = (int)Math.Ceiling(totalMovies / (double)query.PageSize);

        if (query.PageNumber > totalPages)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageNumber", "The page number is out of range"));
        }

        var tvSeriesDtos = bookmarkedTVSeries.Select(movie => new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Year = movie.Year,
            Category = movie.Category,
            Rating = movie.Rating,
            ImgSmall = movie.ImgSmall,
            ImgMedium = movie.ImgMedium,
            ImgLarge = movie.ImgLarge,
            IsBookmarked = true
        });

        var pagedResponse = new PagedResponse<MovieDto>
        {
            Data = tvSeriesDtos,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalPages = totalPages,
            TotalRecords = totalMovies
        };

        return Result<PagedResponse<MovieDto>>.Success(pagedResponse);
    }
}
