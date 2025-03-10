namespace Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Photo { get; set; }

    public string Club { get; set; } = null!;

    public string Microsite { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
