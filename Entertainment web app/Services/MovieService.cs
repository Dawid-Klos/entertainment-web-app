using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using Entertainment_web_app.Common.Responses;
using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Repositories;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MovieService(IMovieRepository movieRepository, IBookmarkRepository bookmarkRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _movieRepository = movieRepository;
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

    public async Task<Result<PagedResponse<MovieDto>>> GetAll(PaginationQuery query)
    {
        if (query.PageSize < 1 || query.PageSize > 20)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageSize", "The page size is out of range"));
        }

        if (query.PageNumber < 1)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageNumber", "The page number is out of range"));
        }

        var totalMovies = await _movieRepository.CountAll();
        var totalPages = (int)Math.Ceiling(totalMovies / (double)query.PageSize);

        if (query.PageNumber > totalPages)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("NotFound", "No movies found"));
        }

        var movies = await _movieRepository.GetAllPaginated(query.PageNumber, query.PageSize);

        if (movies == null)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("NotFound", "No movies found"));
        }

        var userBookmarks = await GetBookmarkedMoviesIds();

        var movieDtos = movies.Select(m => new MovieDto
        {
            MovieId = m.MovieId,
            Title = m.Title,
            Year = m.Year,
            Category = m.Category,
            Rating = m.Rating,
            ImgSmall = m.ImgSmall,
            ImgMedium = m.ImgMedium,
            ImgLarge = m.ImgLarge,
            IsBookmarked = userBookmarks?.Contains(m.MovieId) ?? false
        }).ToList();


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

    public async Task<Result<PagedResponse<MovieDto>>> GetByCategory(MediaCategory category, PaginationQuery query)
    {
        if (query.PageSize < 1 || query.PageSize > 20)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageSize", "The page size is out of range"));
        }

        var totalMovies = await _movieRepository.CountByCategory(category.ToString());
        var totalPages = (int)Math.Ceiling(totalMovies / (double)query.PageSize);

        if (query.PageNumber < 1 || query.PageNumber > totalPages)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageNumber", "The page number is out of range"));
        }

        var movies = await _movieRepository.GetByCategoryPaginated(category.ToString(), query.PageNumber, query.PageSize);

        if (movies == null)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("NotFound", "No movies found"));
        }

        var userBookmarks = await GetBookmarkedMoviesIds();

        var movieDtos = movies.Select(m => new MovieDto
        {
            MovieId = m.MovieId,
            Title = m.Title,
            Year = m.Year,
            Category = m.Category,
            Rating = m.Rating,
            ImgSmall = m.ImgSmall,
            ImgMedium = m.ImgMedium,
            ImgLarge = m.ImgLarge,
            IsBookmarked = userBookmarks?.Contains(m.MovieId) ?? false
        }).ToList();

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

    public async Task<Result<IEnumerable<MovieDto>>> Search(MediaCategory category, SearchQuery query)
    {
        var movies = await _movieRepository.GetByCategory(category.ToString());

        if (movies == null)
        {
            return Result<IEnumerable<MovieDto>>.Failure(new Error("NotFound", "No movies found"));
        }

        if (!string.IsNullOrWhiteSpace(query.Title))
        {
            movies = movies.Where(m => m.Title.ToLower().Contains(query.Title.ToLower()));
        }

        if (query.Year != null)
        {
            movies = movies.Where(m => m.Year == query.Year);
        }

        if (query.Rating != null)
        {
            movies = movies.Where(m => m.Rating == query.Rating);
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            movies = query.SortBy switch
            {
                "title" => query.IsAscending
                    ? movies.OrderBy(m => m.Title)
                    : movies.OrderByDescending(m => m.Title),
                "year" => query.IsAscending
                    ? movies.OrderBy(m => m.Year)
                    : movies.OrderByDescending(m => m.Year),
                "rating" => query.IsAscending
                    ? movies.OrderBy(m => m.Rating)
                    : movies.OrderByDescending(m => m.Rating),
                _ => movies
            };
        }

        var userBookmarks = await GetBookmarkedMoviesIds();

        var movieDtos = movies.Select(m => new MovieDto
        {
            MovieId = m.MovieId,
            Title = m.Title,
            Year = m.Year,
            Category = m.Category,
            Rating = m.Rating,
            ImgSmall = m.ImgSmall,
            ImgMedium = m.ImgMedium,
            ImgLarge = m.ImgLarge,
            IsBookmarked = userBookmarks?.Contains(m.MovieId) ?? false
        }).ToList();

        return Result<IEnumerable<MovieDto>>.Success(movieDtos);
    }

    public async Task<Result<MovieDto>> GetById(MediaCategory category, int movieId)
    {
        var movie = await _movieRepository.GetById(movieId);

        if (movie == null)
        {
            return Result<MovieDto>.Failure(new Error("NotFound", $"Movie with ID = {movieId} does not exist"));
        }

        if (movie.Category != category.ToString())
        {
            return Result<MovieDto>.Failure(new Error("InvalidCategory", "Movie category does not match the specified category"));
        }

        var userBookmarks = await GetBookmarkedMoviesIds();

        var movieDto = new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Year = movie.Year,
            Category = movie.Category,
            Rating = movie.Rating,
            ImgSmall = movie.ImgSmall,
            ImgMedium = movie.ImgMedium,
            ImgLarge = movie.ImgLarge,
            IsBookmarked = userBookmarks?.Contains(movie.MovieId) ?? false
        };

        return Result<MovieDto>.Success(movieDto);
    }

    public async Task<Result> Add(Movie movie)
    {
        var movieExists = await _movieRepository.GetById(movie.MovieId);

        if (movieExists != null)
        {
            return Result.Failure(new Error("AlreadyExists", "Movie with the same ID already exists"));
        }

        bool isValidCategory = Enum.TryParse<MediaCategory>(movie.Category, out _);

        if (!isValidCategory)
        {
            return Result.Failure(new Error("InvalidCategory", "Ensure the category is correct"));
        }

        await _movieRepository.Add(movie);

        return Result.Success();
    }

    public async Task<Result> Update(Movie movie)
    {
        var movieExists = await _movieRepository.GetById(movie.MovieId);

        if (movieExists == null)
        {
            return Result.Failure(new Error("NotFound", "Movie with the specified ID does not exist"));
        }

        bool isValidCategory = Enum.TryParse<MediaCategory>(movie.Category, out _);

        if (!isValidCategory)
        {
            return Result.Failure(new Error("InvalidCategory", "Ensure the category is correct"));
        }

        await _movieRepository.Update(movie);

        return Result.Success();
    }

    public async Task<Result> Delete(int movieId)
    {
        var movieExists = await _movieRepository.GetById(movieId);

        if (movieExists == null)
        {
            return Result.Failure(new Error("NotFound", "Movie with the specified ID does not exist"));
        }

        await _movieRepository.Delete(movieId);

        return Result.Success();
    }
}
