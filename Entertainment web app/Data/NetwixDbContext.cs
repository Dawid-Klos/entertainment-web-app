using Duende.IdentityServer.EntityFramework.Options;
using Entertainment_web_app.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Entertainment_web_app.Data;

public class NetwixDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    
    public NetwixDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)  : base(options,operationalStoreOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserMovies>()
            .HasKey(e => new { e.Id, e.MovieId });
    }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<UserMovies> UserMovies { get; set; }
    
}