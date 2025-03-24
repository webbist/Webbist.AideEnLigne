using AssistClub.UI.Blazor.Constants;
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
            var result = await http.GetFromJsonAsync<UserApiResponse>(ApiRoutes.Users.GetUserByEmail(email));
            if (result != null)
                return new UserViewModel
                {
                    Id = result.Id,
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
    
    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="user">The user details to create.</param>
    /// <returns><c>true</c> if the user was created successfully, <c>false</c> otherwise.</returns>
    public async Task<bool> CreateUser(UserApiRequest user)
    {
        try
        {
            var response = await http.PostAsJsonAsync(ApiRoutes.Users.CreateUser, user);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating user: {e.Message}");
            return false;
        }
    }
}