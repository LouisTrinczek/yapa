namespace Domain.Entities;

public class PollOption : BaseEntity
{
    public PollOption(Guid pollId, string text, int position, int voteCount)
    {
        PollId = pollId;
        Text = text;
        Position = position;
        VoteCount = voteCount;
    }

    public Guid PollId { get; private set; }
    public Poll? Poll { get; private set; }
    public string Text { get; private set; }
    public int Position { get; private set; }
    public int VoteCount { get; private set; } = 0;
    
    public List<Vote> Votes { get; private set; } = new List<Vote>();

    public void Update(string text, int position, int voteCount)
    {
        OnUpdate();
        Text = text;
        Position = position;
        VoteCount = voteCount;
    }
}