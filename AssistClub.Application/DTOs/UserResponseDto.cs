namespace AssistClub.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing the user data that is returned by the API.
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