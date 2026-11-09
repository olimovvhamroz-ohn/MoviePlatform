using Microsoft.EntityFrameworkCore;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public static class ConfigureReview
{
    public static void ConfigureReviewEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReviewEntity>(e =>
        {
            e.ToTable("reviews");
            e.HasKey(x => x.Id);
            e.Property(x => x.Comment).HasMaxLength(2000);
            e.HasIndex(x => new { x.UserId, x.MovieId })
                .IsUnique()
                .HasFilter(DbConstants.NotDeletedFilter);
            e.HasIndex(x => x.MovieId);
            e.HasQueryFilter(x => !x.IsDeleted);
        });
    }
}