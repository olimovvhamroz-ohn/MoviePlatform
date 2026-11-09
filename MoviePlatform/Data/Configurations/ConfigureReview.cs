using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public class ConfigureReview : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.ToTable("reviews");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Comment).HasMaxLength(2000);

        builder.HasIndex(x => new { x.UserId, x.MovieId })
            .IsUnique()
            .HasFilter(DbConstants.NotDeletedFilter);

        builder.HasIndex(x => x.MovieId);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}