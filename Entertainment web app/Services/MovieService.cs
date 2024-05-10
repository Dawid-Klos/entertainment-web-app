using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Models.Responses;
using Entertainment_web_app.Repositories;

namespace Entertainment_web_app.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Result<IEnumerable<MovieDto>>> GetAll()
    {
        var movies = await _movieRepository.GetByCategory("Movies");

        if (movies == null)
        {
            return Result<IEnumerable<MovieDto>>.Failure(new Error("NotFound", "No movies found"));
        }

        var data = movies.Select(m => new MovieDto
        {
            MovieId = m.MovieId,
            Title = m.Title,
            Year = m.Year,
            Category = m.Category,
            Rating = m.Rating,
            ImgSmall = m.ImgSmall,
            ImgMedium = m.ImgMedium,
            ImgLarge = m.ImgLarge
        });

        return Result<IEnumerable<MovieDto>>.Success(data);
    }

    public async Task<Result<PagedResponse<MovieDto>>> GetAllPaginated(int pageNumber, int pageSize)
    {
        if (pageSize < 1 || pageSize > 20)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageSize", "The page size is out of range"));
        }

        var totalMovies = await _movieRepository.CountByCategory("Movies");
        var totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

        if (pageNumber < 1 || pageNumber > totalPages)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("InvalidPageNumber", "The page number is out of range"));
        }

        var movies = await _movieRepository.GetByCategoryPaginated("Movies", pageNumber, pageSize);

        if (movies == null)
        {
            return Result<PagedResponse<MovieDto>>.Failure(new Error("NotFound", "No movies found"));
        }

        var movieDtos = movies.Select(m => new MovieDto
        {
            MovieId = m.MovieId,
            Title = m.Title,
            Year = m.Year,
            Category = m.Category,
            Rating = m.Rating,
            ImgSmall = m.ImgSmall,
            ImgMedium = m.ImgMedium,
            ImgLarge = m.ImgLarge
        }).ToList();

        var pagedResponse = new PagedResponse<MovieDto>
        {
            Data = movieDtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalRecords = totalMovies
        };

        return Result<PagedResponse<MovieDto>>.Success(pagedResponse);
    }

    public async Task<Result<MovieDto>> GetById(int movieId)
    {
        var movie = await _movieRepository.GetById(movieId);

        if (movie == null || movie.Category != "Movies")
        {
            return Result<MovieDto>.Failure(new Error("NotFound", $"Movie with ID = {movieId} does not exist"));
        }

        var movieDto = new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Year = movie.Year,
            Category = movie.Category,
            Rating = movie.Rating,
            ImgSmall = movie.ImgSmall,
            ImgMedium = movie.ImgMedium,
            ImgLarge = movie.ImgLarge
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

        if (movie.Category != "Movies")
        {
            return Result.Failure(new Error("InvalidCategory", "Category must be 'Movies'"));
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

        if (movie.Category != "Movies")
        {
            return Result.Failure(new Error("InvalidCategory", "Category must be 'Movies'"));
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
