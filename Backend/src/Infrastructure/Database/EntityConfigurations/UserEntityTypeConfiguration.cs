using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ConfigureBaseEntity(); 
       builder.Property(x => x.Username).HasMaxLength(30).IsRequired();
       builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
       builder.Property(x => x.PasswordHash).HasMaxLength(100).IsRequired();
       builder.Property(x => x.Salt).IsRequired();
    }
}