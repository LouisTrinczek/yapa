using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfigurations;

public class VoteEntityTypeConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ConfigureBaseEntity();
        builder.Property(x => x.PollId).IsRequired();
        builder.Property(x => x.OptionId).IsRequired();
        builder.Property(x => x.UserId);
        builder.Property(x => x.VoterIdentifier);

        builder.HasOne(x => x.Poll);
        builder.HasOne(x => x.PollOption).WithMany(x => x.Votes);
        builder.HasOne(x => x.User);
    }
}