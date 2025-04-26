namespace AssistClub.Application.DTOs;

/// <summary>
/// Represents a request to send an email.
/// </summary>
public class EmailRequest
{
    /// <summary>
    /// The email address of the recipient.
    /// </summary>
    public string? To { get; set; }
    
    /// <summary>
    /// The subject of the email.
    /// </summary>
    public string? Subject { get; set; }
    
    /// <summary>
    /// The body of the email.
    /// </summary>
    public string? Body { get; set; }
}