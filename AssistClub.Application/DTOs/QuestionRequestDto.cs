namespace AssistClub.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing the request structure for creating or updating a question.
/// This DTO ensures that only valid and necessary data is received from the client.
/// </summary>
public class QuestionRequestDto
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Visibility { get; set; }
    public string Status { get; set; }
}