using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Repositories;

namespace Entertainment_web_app.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<Movie>> GetAll()
    {
        try
        {
            var movies = await _movieRepository.GetByCategory("Movie");

            return movies;
        }
        catch (Exception)
        {
            throw new Exception("No movies found");
        }
    }

    public async Task<PagedResponse<Movie>> GetAllPaginated(int pageNumber, int pageSize)
    {
        var totalMovies = await _movieRepository.CountByCategory("Movie");
        var totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

        if (pageNumber < 1 || pageNumber > totalPages)
        {
            throw new ArgumentException("Invalid page number");
        }

        var movies = await _movieRepository.GetByCategoryPaginated("Movie", pageNumber, pageSize);

        return new PagedResponse<Movie>
        {
            Data = movies.ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }

    public async Task<Movie> GetById(int movieId)
    {
        try
        {
            var movie = await _movieRepository.GetById(movieId);

            if (movie.Category != "Movie" || movie == null)
            {
                throw new ArgumentException($"Movie with ID = {movieId} does not exist");
            }

            return movie;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public async void Add(Movie movie)
    {
        try
        {
            var movieExists = await _movieRepository.GetById(movie.MovieId);

            if (movieExists != null)
            {
                throw new Exception("Movie already exists");
            }

            if (movie.Category != "Movie")
            {
                throw new ArgumentException("Invalid category");
            }

            _movieRepository.Add(movie);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async void Update(Movie movie)
    {
        try
        {
            var movieExists = await _movieRepository.GetById(movie.MovieId);

            if (movieExists == null)
            {
                throw new Exception("Movie not found");
            }

            _movieRepository.Update(movie);
        }
        catch (Exception)
        {
            throw new Exception("Movie does not exist");
        }
    }

    public async void Delete(int movieId)
    {
        try
        {
            var movieExists = await _movieRepository.GetById(movieId);

            if (movieExists == null)
            {
                throw new Exception("Movie not found");
            }

            _movieRepository.Delete(movieId);
        }
        catch (Exception)
        {
            throw new Exception("Movie does not exist");
        }
    }
}
