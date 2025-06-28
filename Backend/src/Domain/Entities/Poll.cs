namespace Domain.Entities;

public class Poll : BaseEntity
{
    public Poll(string title, string? description, string shareSlug, bool allowMultipleChoice, DateTime? deadline,
        string voteProtection)
    {
        Title = title;
        Description = description;
        ShareSlug = shareSlug;
        AllowMultipleChoice = allowMultipleChoice;
        Deadline = deadline;
        VoteProtection = voteProtection;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string ShareSlug { get; private set; }
    public bool AllowMultipleChoice { get; private set; }
    public DateTime? Deadline { get; private set; }
    public string VoteProtection { get; private set; }
    
    public List<PollOption> Options { get; private set; } = new List<PollOption>();

    public void Update(string title, string? description, bool allowMultipleChoice,
        DateTime? deadline, string voteProtection)
    {
        OnUpdate();
        Title = title;
        Description = description;
        AllowMultipleChoice = allowMultipleChoice;
        Deadline = deadline;
        VoteProtection = voteProtection;
    }
}