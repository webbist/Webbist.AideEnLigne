using AssistClub.UI.Blazor.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace AssistClub.UI.Blazor.Extensions;

/// <summary>
/// Provides helper methods for localizing question statuses.
/// </summary>
public static class QuestionStatusExtensions
{
    private static readonly IStringLocalizer DefaultLocalizer =
        new ResourceManagerStringLocalizerFactory(
            new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
            NullLoggerFactory.Instance
        ).Create(typeof(QuestionStatusResources));

    /// <summary>
    /// Converts a question status string into a localized label using the provided localizer.
    /// </summary>
    /// <param name="status">The status string to be localized.</param>
    /// <returns>A localized string representing the status.</returns>
    public static string ToStatusLocalizedLabel(this string status)
    {
        return status.ToStatusLocalizedLabel(DefaultLocalizer);
    }
    
    /// <summary>
    /// Converts a question status string into a localized label using the provided localizer.
    /// </summary>
    /// <param name="status">The status string to be localized.</param>
    /// <param name="localizer">The localization service.</param>
    /// <returns>A localized string representing the status.</returns>
    public static string ToStatusLocalizedLabel(this string status, IStringLocalizer localizer)
    {
        return status switch
        {
            "open" => localizer["StatusOpenLabel"],
            "pending" => localizer["StatusPendingLabel"],
            "resolved" => localizer["StatusResolvedLabel"],
            _ => localizer["StatusUnknownLabel"]
        };
    }
}