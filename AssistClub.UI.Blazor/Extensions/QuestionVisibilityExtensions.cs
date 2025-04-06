using AssistClub.UI.Blazor.Models;
using AssistClub.UI.Blazor.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace AssistClub.UI.Blazor.Extensions;

/// <summary>
/// Provides helper methods for localizing question visibility.
/// </summary>
public static class QuestionVisibilityExtensions
{
    private static readonly IStringLocalizer DefaultLocalizer =
        new ResourceManagerStringLocalizerFactory(
            new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
            NullLoggerFactory.Instance
        ).Create(typeof(QuestionVisibilityResources));
    
    /// <summary>
    /// Converts a question visibility enum into a localized label using the default localizer.
    /// </summary>
    /// <param name="visibility">The visibility enum to be localized.</param>
    /// <returns>A localized string representing the visibility.</returns>
    public static string ToVisibilityLocalizedLabel(this QuestionVisibility visibility)
    {
        return visibility.ToVisibilityLocalizedLabel(DefaultLocalizer);
    }
    
    /// <summary>
    /// Converts a question visibility enum into a localized label using the provided localizer.
    /// </summary>
    /// <param name="visibility">The visibility enum to be localized.</param>
    /// <param name="localizer">The localization service.</param>
    /// <returns>A localized string representing the visibility.</returns>
    public static string ToVisibilityLocalizedLabel(this QuestionVisibility visibility, IStringLocalizer localizer)
    {
        return visibility switch
        {
            QuestionVisibility.Public => localizer["VisibilityPublic"],
            QuestionVisibility.Private => localizer["VisibilityPrivate"],
            _ => visibility.ToString()
        };
    }
}