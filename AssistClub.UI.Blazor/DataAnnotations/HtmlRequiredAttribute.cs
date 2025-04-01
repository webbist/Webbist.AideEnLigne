using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AssistClub.UI.Blazor.DataAnnotations;

public class HtmlRequiredAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var html = value as string;
        if (string.IsNullOrWhiteSpace(html)) return false;

        var withoutTags = Regex.Replace(html, "<.*?>", string.Empty);
        var decoded = System.Net.WebUtility.HtmlDecode(withoutTags);
        var cleaned = decoded.Replace("&nbsp;", string.Empty).Trim();

        return !string.IsNullOrWhiteSpace(cleaned);
    }
}