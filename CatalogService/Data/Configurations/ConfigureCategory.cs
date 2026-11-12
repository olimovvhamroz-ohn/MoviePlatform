using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public class ConfigureCategory : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(c => c.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);

        builder.HasIndex(x => x.Title).IsUnique().HasFilter(DbConstants.NotDeletedFilter);

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}