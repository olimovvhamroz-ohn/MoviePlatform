using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public class ConfigureActor : IEntityTypeConfiguration<ActorEntity>
{
    public void Configure(EntityTypeBuilder<ActorEntity> builder)
    {
        builder.ToTable("actors");

        builder.HasKey(m => m.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

        builder.Property(x => x.Biography).HasMaxLength(4000);

        builder.HasIndex(x => x.Name);

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}