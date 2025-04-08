using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace AssistClub.UI.Blazor.Extensions;

public static class AuthenticationStateExtensions
{
    public static bool UserIsAdmin(this AuthenticationState auth) => auth.User.IsInRole("Admin");
    
    public static bool UserIsAuthor(this AuthenticationState auth, Guid authorId)
        => auth.User.FindFirst(ClaimTypes.NameIdentifier)?.Value == authorId.ToString();
}