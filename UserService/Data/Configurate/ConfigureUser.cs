using Microsoft.EntityFrameworkCore;
using UserService.Entities;

namespace UserService.Data.Configurate;

public static class ConfigureUser
{
    public static void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(e =>
        {
            e.ToTable("users");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(150);
            e.Property(x => x.Email).IsRequired().HasMaxLength(256);
            e.Property(x => x.Password).IsRequired();
            e.HasIndex(x => x.Email).IsUnique().HasFilter(DbConstants.NotDeletedFilter);
            e.HasQueryFilter(x => !x.IsDeleted);
        });
    }
}