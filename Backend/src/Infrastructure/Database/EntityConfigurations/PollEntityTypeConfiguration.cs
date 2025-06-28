using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfigurations;

public class PollEntityTypeConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.ConfigureBaseEntity();
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ShareSlug).HasMaxLength(100).IsRequired();
        builder.Property(x => x.AllowMultipleChoice).IsRequired();
        builder.Property(x => x.Deadline);
        builder.Property(x => x.VoteProtection).HasMaxLength(100).IsRequired();

        builder.HasMany(x => x.Options).WithOne(x => x.Poll);
    }
}