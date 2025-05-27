using System.Data;

namespace Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public DateTime? ArchivedAt { get; protected set; }

    protected void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public virtual void Archive() => ArchivedAt = DateTime.UtcNow;
}