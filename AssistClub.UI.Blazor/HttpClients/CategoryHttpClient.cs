using AssistClub.UI.Blazor.Models;
using AssistClub.UI.Blazor.Routing;

namespace AssistClub.UI.Blazor.HttpClients;

/// <summary>
/// Represents the client responsible for interacting with the Category API.
/// </summary>
/// <param name="http"><c>HttpClient</c> instance used for sending requests.</param>
public class CategoryHttpClient(HttpClient http)
{
    /// <summary>
    /// Sends a request to retrieve all categories in the system filtered by a search term.
    /// </summary>
    /// <param name="term">The search term to filter categories.</param>
    /// <returns>
    /// A collection of <see cref="CategoryApiResponse"/> entities if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<IEnumerable<CategoryApiResponse>?> GetCategoriesAsync(string? term = null)
    {
        try
        {
            var url = CategoryApiRouting.GetAllRoute;
            if (!string.IsNullOrEmpty(term))
            {
                var query = $"?$filter=contains(Name, '{term}')";
                url += query;
            }
            var result = await http.GetFromJsonAsync<IEnumerable<CategoryApiResponse>>(url);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting categories: {e.Message}");
            return null;
        }
    }
}