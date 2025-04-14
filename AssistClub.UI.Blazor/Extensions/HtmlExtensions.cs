using System.Net;
using System.Text.RegularExpressions;

namespace AssistClub.UI.Blazor.Extensions;

public static class HtmlExtensions
{
    public static string GetPlainTextFromHtml(this string html)
    {
        if (string.IsNullOrEmpty(html)) return string.Empty;
        string decoded = WebUtility.HtmlDecode(html);
        return Regex.Replace(decoded, "<.*?>", " ");
    }
}