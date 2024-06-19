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

    public async Task<Result<IEnumerable<MovieDto>>> Search(string userId, MediaCategory category, SearchQuery query)
    {
        var bookmarks = await _bookmarkRepository.GetByCategoryAndUserId(category.ToString(), userId);

        if (bookmarks == null)
        {
            return Result<IEnumerable<MovieDto>>.Failure(new Error("NotFound", $"No bookmarked content found for {category}"));
        }

        var movieIds = bookmarks.Select(b => b.MovieId);
        var bookmarkedContent = await _movieRepository.GetByIds(movieIds);

        if (bookmarkedContent == null)
        {
            return Result<IEnumerable<MovieDto>>.Failure(new Error("NotFound", $"No bookmarked content found for {category}"));
        }

        if (!string.IsNullOrWhiteSpace(query.Title))
        {
            bookmarkedContent = bookmarkedContent.Where(m => m.Title.ToLower().Contains(query.Title.ToLower()));
        }

        if (query.Year != null)
        {
            bookmarkedContent = bookmarkedContent.Where(m => m.Year == query.Year);
        }

        if (query.Rating != null)
        {
            bookmarkedContent = bookmarkedContent.Where(m => m.Rating == query.Rating);
        }


        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            query.SortBy = query.SortBy.ToLower();

            bookmarkedContent = query.SortBy switch
            {
                "title" => query.IsAscending
                    ? bookmarkedContent.OrderBy(m => m.Title)
                    : bookmarkedContent.OrderByDescending(m => m.Title),
                "year" => query.IsAscending
                    ? bookmarkedContent.OrderBy(m => m.Year)
                    : bookmarkedContent.OrderByDescending(m => m.Year),
                "rating" => query.IsAscending
                    ? bookmarkedContent.OrderBy(m => m.Rating)
                    : bookmarkedContent.OrderByDescending(m => m.Rating),
                _ => bookmarkedContent
            };
        }

        var movieDtos = bookmarkedContent.Select(m => new MovieDto
        {
            MovieId = m.MovieId,
            Title = m.Title,
            Year = m.Year,
            Category = m.Category,
            Rating = m.Rating,
            ImgSmall = m.ImgSmall,
            ImgMedium = m.ImgMedium,
            ImgLarge = m.ImgLarge,
            IsBookmarked = true
        }).ToList();

        return Result<IEnumerable<MovieDto>>.Success(movieDtos);
    }
}
