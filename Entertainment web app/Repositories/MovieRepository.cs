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

    public async Task<IEnumerable<Movie>> GetAllPaginated(int pageNumber, int pageSize)
    {
        return await _context.Movies
          .Skip((pageNumber - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetByCategory(string category)
    {
        return await _context.Movies.Where(m => m.Category == category).ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetByCategoryPaginated(string category, int pageNumber, int pageSize)
    {
        return await _context.Movies.Where(m => m.Category == category)
          .Skip((pageNumber - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();
    }

    public async Task<Movie?> GetById(int movieId)
    {
        return await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.MovieId == movieId);
    }

    public async Task Add(Movie movie)
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

    public async Task Update(Movie movie)
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

    public async Task Delete(int movieId)
    {
        var movie = await _context.Movies.FindAsync(movieId);

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Movies.Remove(movie!);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception($"Error deleting movie: {ex.Message}");
        }
    }

    public async Task<int> CountAll()
    {
        return await _context.Movies.CountAsync();
    }

    public async Task<int> CountByCategory(string category)
    {
        return await _context.Movies.Where(m => m.Category == category).CountAsync();
    }
}
