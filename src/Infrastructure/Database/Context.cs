using Microsoft.EntityFrameworkCore;
using Infrastructure.Database.Entities;

namespace Infrastructure.Database;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }
    public DbSet<Watchlist> Watchlist { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.Property(o => o.FilmId).HasMaxLength(32);
            entity.Property(o => o.Title).HasMaxLength(512);
            entity.Property(o => o.Type).HasDefaultValue(WatchlistType.Unwanted);
        });
    }
}