namespace AssistClub.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing the response structure for a question.
/// This DTO ensures that only relevant question details are exposed to the client.
/// </summary>
public class QuestionResponseDto
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Visibility { get; set; }
    public string Status { get; set; }
}