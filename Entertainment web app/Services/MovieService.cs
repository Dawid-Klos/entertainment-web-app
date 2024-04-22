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
        return await _movieRepository.GetAll();
    }

    public async Task<PagedResponse<Movie>> GetAllPaginated(int pageNumber, int pageSize)
    {
        var totalMovies = _movieRepository.CountAll();
        var totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

        if (pageNumber < 1 || pageNumber > totalPages)
        {
            throw new Exception("Invalid page number");
        }

        var movies = await _movieRepository.GetAllPaginated(pageNumber, pageSize);

        return new PagedResponse<Movie>
        {
            Data = movies.ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }
}
