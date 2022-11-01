using Entertainment_web_app.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Entertainment_web_app.Data;

public class NetwixDbContext : IdentityDbContext<ApplicationUser>
{
    
    public NetwixDbContext(DbContextOptions<NetwixDbContext> options)  : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

}