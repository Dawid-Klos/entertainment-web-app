using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Models.User;

namespace Entertainment_web_app.Data;

public class NetwixDbContext : IdentityDbContext<ApplicationUser>
{

    public NetwixDbContext(DbContextOptions<NetwixDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(e => e.Bookmarks)
            .WithMany(e => e.Users)
            .UsingEntity<Bookmark>();

        base.OnModelCreating(modelBuilder);
    }

    public new DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<Trending> Trending { get; set; }
}
