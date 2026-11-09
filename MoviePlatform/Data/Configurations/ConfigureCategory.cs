using Microsoft.EntityFrameworkCore;
using MoviePlatform.Entities;

namespace MoviePlatform.Data.Configurations;

public class ConfigureCategory
{
    public  static void ConfigureCategoryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>(e =>
            {


                e.ToTable("categories");
                e.HasKey(c => c.Id);
                e.Property(x => x.Title).IsRequired().HasMaxLength(50);
                e.HasIndex(x => x.Title).IsUnique().HasFilter(DbConstants.NotDeletedFilter);
                e.HasQueryFilter(c => !c.IsDeleted);
            }
        );
    }
}