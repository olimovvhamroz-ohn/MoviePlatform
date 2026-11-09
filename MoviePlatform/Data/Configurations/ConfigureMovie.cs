using Microsoft.EntityFrameworkCore;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public class ConfigureMovie
{
    public static void ConfigureMovieEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieEntity>(e =>
        {
            e.ToTable("movies");
            e.HasKey(m => m.Id);
            e.Property(c => c.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Description).HasMaxLength(2000);
            e.Property(x => x.Price).HasPrecision(18, 2);
            e.Property(x => x.AgeRating).HasConversion<string>().HasMaxLength(20);
            e.HasOne(x => x.Category)
                .WithMany(c => c.Movies)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Studio)
                .WithMany(c => c.Movies)
                .HasForeignKey(x => x.StudioId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasMany(x => x.Actors).WithMany().UsingEntity(j => j.ToTable("movie_actors"));
            e.HasIndex(x => new { x.Title, x.ReleaseYear })
                .IsUnique()
                .HasFilter(DbConstants.NotDeletedFilter);
            e.HasIndex(x => x.CategoryId);
            e.HasIndex(x => x.StudioId);
            e.HasQueryFilter(c => !c.IsDeleted);
        });
    }
}