using Microsoft.EntityFrameworkCore;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public class ConfigureActor
{
    public static void ActorConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorEntity>(e =>
        {
            e.ToTable("actors");
            e.HasKey(m => m.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(150);
            e.Property(x => x.Biography).HasMaxLength(4000);
            e.HasIndex(x => x.Name);
            e.HasQueryFilter(c => !c.IsDeleted);
        });
    }
}