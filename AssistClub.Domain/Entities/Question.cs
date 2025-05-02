namespace Domain.Entities;

public partial class Question
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Visibility { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? AttachmentName { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
