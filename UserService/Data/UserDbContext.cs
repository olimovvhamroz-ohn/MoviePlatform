using Microsoft.EntityFrameworkCore;
using UserService.Data.Configurations;
using UserService.Entities;

namespace UserService.Data;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RefreshTokenEntity> RefreshToken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUser.ConfigureUserEntity(modelBuilder);
        ConfigureRefreshToken.ConfigureRefreshTokenEntity(modelBuilder);
    }
}