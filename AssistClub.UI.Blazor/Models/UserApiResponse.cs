namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the user data returned from the API for display in the UI.
/// This class ensures the UI is decoupled from the API's internal DTOs.
/// </summary>
public class UserApiResponse
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string? Photo { get; set; }
    public string Club { get; set; }
    public string Microsite { get; set; }
}