namespace AssistClub.Application.DTOs;

/// <summary>
/// Data transfer object for user responses.
/// </summary>
public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string? Photo { get; set; }
    public string Club { get; set; }
    public string Microsite { get; set; }
}