using AssistClub.UI.Blazor.Models;
using AssistClub.UI.Blazor.Routing;

namespace AssistClub.UI.Blazor.HttpClients;

/// <summary>
/// Represents the client responsible for interacting with the Answer Vote API.
/// </summary>
/// <param name="http"><c>HttpClient</c> instance used for sending requests.</param>
public class AnswerVoteHttpClient(HttpClient http)
{
    /// <summary>
    /// Sends a request to toggle the vote for a specific answer by a user.
    /// </summary>
    /// <param name="answerVoteRequest">The <see cref="AnswerVoteRequest"/> containing the user ID and answer ID.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the vote was successfully toggled.
    /// </returns>
    public async Task<bool> ToggleVoteAsync(AnswerVoteRequest answerVoteRequest)
    {
        try
        {
            var response = await http.PostAsJsonAsync(AnswerVoteApiRouting.ToggleVoteRoute, answerVoteRequest);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error toggling vote: {e.Message}");
            return false;
        }
    }
}