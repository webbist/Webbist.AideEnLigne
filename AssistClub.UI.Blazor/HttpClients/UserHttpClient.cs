using AssistClub.UI.Blazor.Models;

namespace AssistClub.UI.Blazor.HttpClients;

/// <summary>
/// Handles communication between the Blazor UI and the API for retrieving user data.
/// Ensures the UI remains decoupled from API response structures.
/// </summary>
public class UserHttpClient(HttpClient http)
{
    /// <summary>
    /// Retrieves a user's profile information by email.
    /// This method is critical for displaying user details in the UI.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>A UserViewModel containing user details, or null if not found.</returns>
    public async Task<UserViewModel?> GetUserByEmail(string email)
    {
        try
        {
            var result = await http.GetFromJsonAsync<UserApiResponse>($"v1/user/{email}");
            if (result != null)
                return new UserViewModel
                {
                    Fullname = result.Firstname + " " + result.Lastname,
                    Email = result.Email,
                    Photo = result.Photo,
                    Club = result.Club,
                    Microsite = result.Microsite
                };
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving user: {e.Message}");
            return null;
        }
    }
}