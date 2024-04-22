using Microsoft.EntityFrameworkCore;

using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public class TrendingRepository
{

    private readonly NetwixDbContext _context;

    public TrendingRepository(NetwixDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trending>> GetAllAsync()
    {
        return await _context.Trending.ToListAsync();
    }

    public async Task<Trending> GetById(int trendingId)
    {
        var trending = await _context.Trending.FindAsync(trendingId);

        if (trending == null)
        {
            throw new Exception($"Trending with ID {trendingId} not found");
        }

        return trending;
    }

    public async void Add(Trending trending)
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

    public async void Update(Trending trending)
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

    public async void Delete(int trendingId)
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

