using Microsoft.EntityFrameworkCore;

namespace Entertainment_web_app.Data;

public class NetwixDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public NetwixDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("NetwixDbContext"));
    }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserMovies> UserMovies { get; set; }
    
}