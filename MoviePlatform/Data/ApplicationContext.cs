using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data.Configurate;
using MoviePlatform.Entities;

namespace MoviePlatform.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<ActorEntity> Actors { get; set; }
    public DbSet<StudioEntity> Studios { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureCategory.ConfigureCategoryEntity(modelBuilder);
        ConfigureMovie.ConfigureMovieEntity(modelBuilder);
        ConfigurateStudio.ConfiguareStudioEntityes(modelBuilder);
        ConfigureActor.ActorConfigure(modelBuilder);
        ConfigureReview.ConfigureReviewEntity(modelBuilder);
        
    }
}