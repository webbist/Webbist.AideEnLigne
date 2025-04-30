namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents a request to set notification preferences for a user.
/// </summary>
public class NotificationPreferenceRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the user whose notification preferences are being set.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be notified about new questions in their club.
    /// </summary>
    public bool NotifyOnNewClubQuestion { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be notified about new answers on their question.
    /// </summary>
    public bool NotifyOnAnswerPublishedOnMyQuestion { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be notified when an answer to their question
    /// is marked as official.
    /// </summary>
    public bool NotifyOnAnswerToMyQuestionMarkedOfficial { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be notified when their question or answer
    /// is modified by an admin.
    /// </summary>
    public bool NotifyOnMyQuestionOrAnswerModifiedByAdmin { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be notified about any official
    /// answer in questions they are related to.
    /// </summary>
    public bool NotifyOnAnyOfficialAnswerInQuestionIrelated { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be notified when a question
    /// they are related to is modified by the author.
    /// </summary>
    public bool NotifyOnQuestionIrelatedModifiedByAuthor { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be notified about new answers
    /// in questions they are related to.
    /// </summary>
    public bool NotifyOnNewAnswerInQuestionIrelated { get; set; }
}