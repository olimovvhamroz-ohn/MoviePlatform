using Microsoft.EntityFrameworkCore;
using UserService.Entities;

namespace UserService.Data.Configurations;

public static class ConfigureRefreshToken
{
    public static void ConfigureRefreshTokenEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshTokenEntity>(e =>
        {
            e.ToTable("RefreshToken");
            e.HasKey(x => x.Id);
            e.Property(x => x.TokenHash).IsRequired();
            e.HasQueryFilter(x => !x.IsDeleted);
        });
    }
}