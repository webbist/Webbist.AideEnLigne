namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the user data returned from the API for display in the UI.
/// </summary>
public class UserApiResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string Firstname { get; set; }
    
    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string Lastname { get; set; }
    
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or sets the URL of the user's profile photo, if available.
    /// </summary>
    public string? Photo { get; set; }
    
    /// <summary>
    /// Gets or sets the club to which the user is affiliated.
    /// </summary>
    public string Club { get; set; }
    
    /// <summary>
    /// Gets or sets the user's microsite URL.
    /// </summary>
    public string Microsite { get; set; }
    
    /// <summary>
    /// The user's role in the system (<c>admin</c> or <c>user</c>).
    /// </summary>
    public string Role { get; set; }
}