using Microsoft.EntityFrameworkCore;

using Entertainment_web_app.Models.Content;
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

    public async Task<Bookmark> GetById(int id)
    {
        var bookmark = await _context.Bookmarks.FindAsync(id);

        if (bookmark == null)
        {
            throw new Exception($"Bookmark with ID = {id} not found");
        }

        return bookmark;
    }

    public async void Add(Bookmark bookmark)
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
            throw new Exception($"Error while adding bookmarkm, {ex.Message}");
        }
    }

    public async void Update(Bookmark bookmark)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Bookmarks.Update(bookmark);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while updating bookmark, {ex.Message}");
        }
    }

    public async void Delete(int id)
    {
        var bookmark = await _context.Bookmarks.FindAsync(id);

        if (bookmark == null)
        {
            throw new Exception($"Bookmark with ID = {id} not found");
        }

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
}

