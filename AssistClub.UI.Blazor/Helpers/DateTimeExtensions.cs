using Microsoft.Extensions.Localization;

namespace AssistClub.UI.Blazor.Helpers;

/// <summary>
/// Provides helper methods for formatting dates and times.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a DateTime into a human-readable "time ago" format.
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <param name="localizer">The localization service.</param>
    /// <returns>A localized string representing the time difference.</returns>
    public static string GetTimeAgo(this DateTime dateTime, IStringLocalizer localizer)
    {
        if (dateTime == null)
        {
            return localizer["TimeAgoUnknown"];
        }

        var timeSpan = DateTime.UtcNow - dateTime;

        if (timeSpan.TotalSeconds < 60)
        {
            return localizer["TimeAgoJustNow"];
        }
        if (timeSpan.TotalMinutes < 60)
        {
            return string.Format(localizer["TimeAgoMinutes"], (int)timeSpan.TotalMinutes);
        }
        if (timeSpan.TotalHours < 24)
        {
            return string.Format(localizer["TimeAgoHours"], (int)timeSpan.TotalHours);
        }
        if (timeSpan.TotalDays < 30)
        {
            return string.Format(localizer["TimeAgoDays"], (int)timeSpan.TotalDays);
        }
        if (timeSpan.TotalDays < 365)
        {
            return string.Format(localizer["TimeAgoMonths"], (int)(timeSpan.TotalDays / 30));
        }

        return string.Format(localizer["TimeAgoYears"], (int)(timeSpan.TotalDays / 365));
    }
}