namespace AssistClub.Application.DTOs;

/// <summary>
/// Represents the user data returned by the API.
/// </summary>
/// <remarks>
/// Ensures that only relevant user details are exposed to the client.
/// </remarks>
public class UserResponseDto
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// The user's first name.
    /// </summary>
    public string Firstname { get; set; }
    /// <summary>
    /// The user's last name.
    /// </summary>  
    public string Lastname { get; set; }
    /// <summary>
    /// The user's email address.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// The URL of the user's profile photo, if available.
    /// </summary>
    public string? Photo { get; set; }
    /// <summary>
    /// The club to which the user is affiliated.
    /// </summary>
    public string Club { get; set; }
    /// <summary>
    /// The user's microsite URL.
    /// </summary>
    public string Microsite { get; set; }
}