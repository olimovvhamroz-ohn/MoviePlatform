using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Entities;

namespace UserService.Data.Configurations;

public class ConfigureUser : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        
        builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
        
        builder.Property(x => x.Password).IsRequired();
        
        builder.HasIndex(x => x.Email).IsUnique().HasFilter(DbConstants.NotDeletedFilter);
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}