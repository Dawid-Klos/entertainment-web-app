using Microsoft.EntityFrameworkCore;

using Entertainment_web_app.Data;
using Entertainment_web_app.Models.User;

namespace Entertainment_web_app.Repositories;

public class UserRepository : IUserRepository
{

    private readonly NetwixDbContext _context;

    public UserRepository(NetwixDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<ApplicationUser?> GetById(string userId)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async void Add(ApplicationUser user)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while adding user, {ex.Message}");
        }
    }

    public async void Update(ApplicationUser user)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while updating user, {ex.Message}");
        }
    }

    public async void Delete(ApplicationUser user)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error while deleting user, {ex.Message}");
        }
    }
}
