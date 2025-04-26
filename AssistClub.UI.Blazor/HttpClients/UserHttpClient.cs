using AssistClub.UI.Blazor.Routing;
using AssistClub.UI.Blazor.Models;

namespace AssistClub.UI.Blazor.HttpClients;

/// <summary>
/// Represents the client responsible for communicating with the API to retrieve user data.
/// </summary>
/// <param name="http"><c>HttpClient</c> instance used for making API requests.</param>
public class UserHttpClient(HttpClient http)
{
    /// <summary>
    /// Retrieves a user's profile information by email.
    /// </summary>
    /// <remarks>
    /// Used to fetch user details for profile display in the UI.
    /// </remarks>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>A <see cref="UserViewModel"/> containing user details, or <c>null</c> if not found.</returns>
    public async Task<UserViewModel?> GetUserByEmail(string email)
    {
        try
        {
            var result = await http.GetFromJsonAsync<UserApiResponse>(UserApiRouting.GetUserByEmailRoute(email));
            if (result != null)
                return new UserViewModel
                {
                    Id = result.Id,
                    Fullname = result.Firstname + " " + result.Lastname,
                    Email = result.Email,
                    Photo = result.Photo,
                    Club = result.Club,
                    Microsite = result.Microsite,
                    Role = Enum.Parse<Role>(result.Role, true)
                };
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving user: {e.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="user">The user details to create.</param>
    /// <returns><c>true</c> if the user was created successfully, <c>false</c> otherwise.</returns>
    public async Task<bool> CreateUser(UserApiRequest user)
    {
        try
        {
            var response = await http.PostAsJsonAsync(UserApiRouting.CreateRoute, user);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating user: {e.Message}");
            return false;
        }
    }

    /// <summary>
    /// Retrieves a user's profile information by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A <see cref="UserViewModel"/> containing user details, or <c>null</c> if not found.</returns>
    public async Task<UserViewModel?> GetUserByIdAsync(Guid id)
    {
        try
        {
            var result = await http.GetFromJsonAsync<UserApiResponse>(UserApiRouting.GetUserByIdRoute(id));
            if (result != null)
                return new UserViewModel
                {
                    Id = result.Id,
                    Fullname = result.Firstname + " " + result.Lastname,
                    Email = result.Email,
                    Photo = result.Photo,
                    Club = result.Club,
                    Microsite = result.Microsite,
                    Role = Enum.Parse<Role>(result.Role, true)
                };
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving user: {e.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Retrieves the notification preferences for a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user whose notification preferences to retrieve.</param>
    /// <returns>
    /// A <see cref="NotificationPreferenceRequest"/> containing the user's notification preferences, or <c>null</c> if not found.
    /// </returns>
    public async Task<NotificationPreferenceRequest?> GetNotificationPreferences(Guid id)
    {
        try
        {
            var result = await http.GetFromJsonAsync<NotificationPreferenceRequest>(UserApiRouting.GetNotificationPreferencesRoute(id));
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving notification preference: {e.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Updates the notification preferences for a user.
    /// </summary>
    /// <param name="notificationPreference">The <see cref="NotificationPreferenceRequest"/> containing the updated notification preferences.</param>
    /// <returns>
    /// <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> UpdateNotificationPreferences(NotificationPreferenceRequest notificationPreference)
    {
        try
        {
            var response = await http.PutAsJsonAsync(UserApiRouting.UpdateNotificationPreferencesRoute, notificationPreference);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating notification preference: {e.Message}");
            return false;
        }
    }
}