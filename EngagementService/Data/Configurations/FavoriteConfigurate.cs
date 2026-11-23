using EngagementService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EngagementService.Data.Configurations;

public class FavoriteConfigurate : IEntityTypeConfiguration<FavoriteEntity>
{
    public void Configure(EntityTypeBuilder<FavoriteEntity> builder)
    {
        builder.ToTable("favorites");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.MovieId).IsRequired();

        builder.HasIndex(x => new { x.UserId, x.MovieId })
            .IsUnique()
            .HasFilter(DbConstants.NotDeletedFilter);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}