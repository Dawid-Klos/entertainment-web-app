using Microsoft.EntityFrameworkCore;

using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public class TrendingRepository : ITrendingRepository
{

    private readonly NetwixDbContext _context;

    public TrendingRepository(NetwixDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trending>> GetAll()
    {
        return await _context
          .Trending
          .Include(t => t.Movie)
          .ToListAsync();
    }

    public async Task<Trending?> GetById(int trendingId)
    {
        return await _context
          .Trending
          .Include(t => t.Movie)
          .FirstOrDefaultAsync(t => t.TrendingId == trendingId);
    }

    public async Task<Trending?> GetByMovieId(int movieId)
    {
        return await _context
          .Trending
          .Include(t => t.MovieId)
          .FirstOrDefaultAsync(t => t.MovieId == movieId);
    }

    public async Task Add(Trending trending)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Trending.Add(trending);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception($"Error adding trending: {ex.Message}");
        }
    }

    public async Task Update(Trending trending)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Trending.Update(trending);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception($"Error updating trending: {ex.Message}");
        }
    }

    public async Task Delete(int trendingId)
    {
        var trending = await _context.Trending.FindAsync(trendingId);

        if (trending == null)
        {
            throw new Exception($"Trending with ID {trendingId} not found");
        }

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Trending.Remove(trending);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception($"Error deleting trending: {ex.Message}");
        }
    }
}

