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
        catch (Exception)
        {
            throw new Exception("No movies found");
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
            TotalPages = totalPages
        };
    }

    public async Task<MovieDto> GetById(int movieId)
    {
        try
        {
            var movie = await _movieRepository.GetById(movieId);

            if (movie.Category != "Movies" || movie == null)
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

            if (movie.Category != "Movies")
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
