using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfigurations;

public class PollOptionEntityTypeConfiguration : IEntityTypeConfiguration<PollOption>
{
    public void Configure(EntityTypeBuilder<PollOption> builder)
    {
        builder.ConfigureBaseEntity();
        builder.Property(x => x.Text).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Position).IsRequired();
        builder.Property(x => x.VoteCount).IsRequired();
        
        // Relation configured in PollEntityTypeConfiguration
    }
}