namespace Domain.Entities;

public partial class NotificationPreference
{
    public Guid UserId { get; set; }

    public bool NotifyOnNewClubQuestion { get; set; }

    public bool NotifyOnAnswerPublishedOnMyQuestion { get; set; }

    public bool NotifyOnAnswerToMyQuestionMarkedOfficial { get; set; }

    public bool NotifyOnMyQuestionOrAnswerModifiedByAdmin { get; set; }

    public bool NotifyOnAnyOfficialAnswerInQuestionIrelated { get; set; }

    public bool NotifyOnQuestionIrelatedModifiedByAuthor { get; set; }

    public bool NotifyOnNewAnswerInQuestionIrelated { get; set; }

    public virtual User User { get; set; } = null!;
}
