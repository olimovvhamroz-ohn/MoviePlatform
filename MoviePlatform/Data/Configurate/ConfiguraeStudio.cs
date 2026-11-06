using Microsoft.EntityFrameworkCore;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurate;

public class ConfigurateStudio
{
    public  static void ConfiguareStudioEntityes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudioEntity>(e =>
        {
            e.ToTable("studio");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => x.Name).IsUnique().HasFilter(DbConstants.NotDeletedFilter);
            e.HasQueryFilter(c => !c.IsDeleted);
        });
    }
}