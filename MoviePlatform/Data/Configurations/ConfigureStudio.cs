using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public class ConfigurateStudio : IEntityTypeConfiguration<StudioEntity>
{
    public void Configure(EntityTypeBuilder<StudioEntity> builder)
    {
        builder.ToTable("studios");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

        builder.HasIndex(x => x.Name).IsUnique().HasFilter(DbConstants.NotDeletedFilter);

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}