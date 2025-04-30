namespace Domain.Entities;

public partial class AnswerVote
{
    public Guid AnswerId { get; set; }

    public Guid UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Answer Answer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
