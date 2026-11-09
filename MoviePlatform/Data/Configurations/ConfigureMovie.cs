using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public class ConfigureMovie : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.ToTable("movies");

        builder.HasKey(m => m.Id);

        builder.Property(c => c.Title).IsRequired().HasMaxLength(200);

        builder.Property(x => x.Description).HasMaxLength(2000);

        builder.Property(x => x.Price).HasPrecision(18, 2);

        builder.Property(x => x.AgeRating).HasConversion<string>().HasMaxLength(20);

        builder.HasOne(x => x.Category)
            .WithMany(c => c.Movies)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Studio)
            .WithMany(c => c.Movies)
            .HasForeignKey(x => x.StudioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Actors).WithMany().UsingEntity(j => j.ToTable("movie_actors"));

        builder.HasIndex(x => new { x.Title, x.ReleaseYear })
            .IsUnique()
            .HasFilter(DbConstants.NotDeletedFilter);

        builder.HasIndex(x => x.CategoryId);

        builder.HasIndex(x => x.StudioId);

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}