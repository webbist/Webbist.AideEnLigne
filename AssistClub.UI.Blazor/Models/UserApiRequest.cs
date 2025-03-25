namespace AssistClub.UI.Blazor.Models;

public class UserApiRequest
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
    public string? Email { get; set; }
    
    /// <summary>
    /// The URL of the user's profile photo, if available.
    /// </summary>
    public string? Photo { get; set; }
    
    /// <summary>
    /// The club to which the user is affiliated.
    /// </summary>
    public string? Club { get; set; }
    
    /// <summary>
    /// The user's microsite URL.
    /// </summary>
    public string? Microsite { get; set; }
    
    /// <summary>
    /// The user's role in the system (<c>admin</c> or <c>user</c>).
    /// </summary>
    public Role Role { get; set; }
}