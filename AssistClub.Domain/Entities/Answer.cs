namespace Domain.Entities;

public partial class Answer
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public Guid UserId { get; set; }

    public string Content { get; set; } = null!;

    public bool IsOfficial { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
