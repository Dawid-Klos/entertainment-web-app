using Entertainment_web_app.Models.Content;

namespace Entertainment_web_app.Repositories;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAll();
    Movie GetById(int movieId);
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(int movieId);
}