using Microsoft.EntityFrameworkCore;

using Entertainment_web_app.Data;

namespace Entertainment_web_app.Repositories;

public class BookmarkRepository : IBookmarkRepository
{
    private readonly NetwixDbContext _context;

    public BookmarkRepository(NetwixDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Bookmark>> GetAll()
    {
        return await _context.Bookmarks.ToListAsync();
    }

    public async Task<IEnumerable<Bookmark>> GetAllPaginated(int pageNumber, int pageSize)
    {
        return await _context.Bookmarks
          .OrderBy(b => b.MovieId)
          .Skip((pageNumber - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();
    }

    public async Task<IEnumerable<Bookmark>?> GetByUserId(string userId)
    {
        return await _context.Bookmarks
          .AsNoTracking()
          .Where(b => b.UserId == userId)
          .ToListAsync();
    }

    public async Task<IEnumerable<Bookmark>?> GetByCategoryAndUserId(string category, string userId)
    {
        return await _context.Bookmarks
          .AsNoTracking()
          .Join(_context.Movies,
              b => b.MovieId,
              m => m.MovieId,
              (b, m) => new { Bookmark = b, Movie = m })
          .Where(x => x.Movie.Category == category && x.Bookmark.UserId == userId)
          .Select(x => x.Bookmark)
          .ToListAsync();
    }

    public async Task<Bookmark?> GetById(string userId, int movieId)
    {
        return await _context.Bookmarks
          .AsNoTracking()
          .FirstOrDefaultAsync(b => b.UserId == userId && b.MovieId == movieId);
    }

    public async Task Add(Bookmark bookmark)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            await _context.Bookmarks.AddAsync(bookmark);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while adding bookmark, {ex.Message}");
        }
    }

    public async Task Delete(Bookmark bookmark)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while deleting bookmark, {ex.Message}");
        }
    }

    public async Task<int> CountAll()
    {
        return await _context.Bookmarks.CountAsync();
    }

    public async Task<int> CountByUserId(string userId)
    {
        return await _context.Bookmarks.CountAsync(b => b.UserId == userId);
    }
}
