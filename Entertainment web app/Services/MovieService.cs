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

    public async Task<IEnumerable<MovieDto>> GetAll()
    {
        try
        {
            var movies = await _movieRepository.GetByCategory("Movies");

            return movies.Select(m => new MovieDto
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
        }
        catch (Exception ex)
        {
            throw new Exception("Internal Server Error, " + ex.Message);
        }
    }

    public async Task<PagedResponse<MovieDto>> GetAllPaginated(int pageNumber, int pageSize)
    {
        var totalMovies = await _movieRepository.CountByCategory("Movies");
        var totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

        if (pageNumber < 1 || pageNumber > totalPages)
        {
            throw new ArgumentException("Invalid page number");
        }

        if (pageSize < 1 || pageSize > 20)
        {
            throw new ArgumentException("Invalid page size");
        }

        var movies = await _movieRepository.GetByCategoryPaginated("Movies", pageNumber, pageSize);

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

        return new PagedResponse<MovieDto>
        {
            Data = movieDtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalRecords = totalMovies
        };
    }

    public async Task<MovieDto> GetById(int movieId)
    {
        try
        {
            var movie = await _movieRepository.GetById(movieId);

            if (movie == null || movie.Category != "Movies")
            {
                throw new ArgumentException($"Movie with ID = {movieId} does not exist");
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

            return movieDto;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Add(Movie movie)
    {
        try
        {
            var movieExists = await _movieRepository.GetById(movie.MovieId);

            if (movieExists != null)
            {
                throw new ArgumentException("Movie already exists");
            }

            if (movie.Category != "Movies")
            {
                throw new ArgumentException("Invalid category");
            }

            await _movieRepository.Add(movie);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Update(Movie movie)
    {
        try
        {
            var movieExists = await _movieRepository.GetById(movie.MovieId);

            if (movieExists == null)
            {
                throw new ArgumentException("Movie not found");
            }

            if (movie.Category != "Movies")
            {
                throw new ArgumentException("Invalid category");
            }

            await _movieRepository.Update(movie);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Delete(int movieId)
    {
        try
        {
            var movieExists = await _movieRepository.GetById(movieId);

            if (movieExists == null)
            {
                throw new ArgumentException("Movie not found");
            }

            await _movieRepository.Delete(movieId);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
