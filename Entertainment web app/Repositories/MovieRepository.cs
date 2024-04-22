using Microsoft.EntityFrameworkCore;

using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public class MovieRepository : IMovieRepository
{

    private readonly NetwixDbContext _context;

    public MovieRepository(NetwixDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAll()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetByCategoryPaginated(string category, int pageNumber, int pageSize)
    {
        return await _context.Movies.Where(m => m.Category == category)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Movie> GetById(int movieId)
    {
        var movie = await _context.Movies.FindAsync(movieId);

        if (movie == null)
        {
            throw new Exception($"Movie with ID {movieId} not found");
        }

        return movie;
    }



    public async void Add(Movie movie)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception($"Error adding movie: {ex.Message}");
        }
    }

    public async void Update(Movie movie)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Movies.Update(movie);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception($"Error updating movie: {ex.Message}");
        }
    }

    public async void Delete(int movieId)
    {
        var movie = await _context.Movies.FindAsync(movieId);

        if (movie == null)
        {
            throw new Exception($"Movie with ID {movieId} not found");
        }

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception($"Error deleting movie: {ex.Message}");
        }
    }
}
