using EngagementService.Data.Configurations;
using EngagementService.Entities;
using Microsoft.EntityFrameworkCore;

namespace EngagementService.Data;

public class EngagementDbContext(
    DbContextOptions<EngagementDbContext> options)
    : DbContext(options)
{
    public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
    public DbSet<FavoriteEntity> Favorites => Set<FavoriteEntity>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FavoriteConfigurate).Assembly);
    }
}