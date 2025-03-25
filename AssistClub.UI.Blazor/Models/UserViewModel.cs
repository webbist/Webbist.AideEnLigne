namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the view model for user data in the UI layer.
/// </summary>
public class UserViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the user's full name.
    /// </summary>
    public string? Fullname { get; set; }
    
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Gets or sets the URL of the user's profile photo, if available.
    /// </summary>
    public string? Photo { get; set; }
    
    /// <summary>
    /// Gets or sets the club to which the user is affiliated.
    /// </summary>
    public string? Club { get; set; }
    
    /// <summary>
    /// Gets or sets the user's microsite URL.
    /// </summary>
    public string? Microsite { get; set; }
}