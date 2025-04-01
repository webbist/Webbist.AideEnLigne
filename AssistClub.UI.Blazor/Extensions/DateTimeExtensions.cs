using AssistClub.UI.Blazor.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace AssistClub.UI.Blazor.Extensions;

/// <summary>
/// Provides helper methods for formatting dates and times.
/// </summary>
public static class DateTimeExtensions
{
    private static readonly IStringLocalizer DefaultLocalizer =
        new ResourceManagerStringLocalizerFactory(
            new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
            NullLoggerFactory.Instance
        ).Create(typeof(DateTimeResources));
    
    /// <summary>
    /// Converts a DateTime into a human-readable "time ago" format using the default localizer.
    /// </summary>
    /// <param name="dateTimeUtc">The date and time in UTC to convert.</param>
    /// <returns>A localized string representing the time difference.</returns>
    public static string GetTimeAgo(this DateTime dateTimeUtc)
    {
        return dateTimeUtc.GetTimeAgo(DefaultLocalizer);
    }
    
    /// <summary>
    /// Converts a DateTime into a human-readable "time ago" format.
    /// </summary>
    /// <param name="dateTimeUtc">The date and time to convert.</param>
    /// <param name="localizer">The localization service.</param>
    /// <returns>A localized string representing the time difference.</returns>
    public static string GetTimeAgo(this DateTime dateTimeUtc, IStringLocalizer localizer)
    {
        if (dateTimeUtc == null)
        {
            return localizer["TimeAgoUnknown"];
        }

        var timeSpan = DateTime.UtcNow - dateTimeUtc;

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