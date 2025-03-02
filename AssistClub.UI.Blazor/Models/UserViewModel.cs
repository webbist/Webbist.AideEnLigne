namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// View model for user data in the UI layer.
/// </summary>
public class UserViewModel
{
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public string? Photo { get; set; }
    public string? Club { get; set; }
    public string? Microsite { get; set; }
}