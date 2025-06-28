namespace Domain.Entities;

public class Vote : BaseEntity
{
    public Vote(Guid pollId, Guid optionId, Guid? userId, string? voterIdentifier)
    {
        PollId = pollId;
        OptionId = optionId;
        UserId = userId;
        VoterIdentifier = voterIdentifier;
    }

    public Guid PollId { get; private set; }
    public Poll? Poll { get; private set; }
    public Guid OptionId { get; private set; }
    public PollOption? PollOption { get; private set; }
    public Guid? UserId { get; private set; }
    public User? User { get; private set; }
    public string? VoterIdentifier { get; private set; }

    public void Update(Guid optionId, string? voterIdentifier)
    {
        OnUpdate();
        OptionId = optionId;
        VoterIdentifier = voterIdentifier;
    }
}