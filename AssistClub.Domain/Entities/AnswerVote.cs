namespace Domain.Entities;

public partial class AnswerVote
{
    public Guid Id { get; set; }

    public Guid AnswerId { get; set; }

    public Guid UserId { get; set; }

    public virtual Answer Answer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
