using AssistClub.UI.Blazor.Models;
using AssistClub.UI.Blazor.Routing;

namespace AssistClub.UI.Blazor.HttpClients;

/// <summary>
/// Represents the client responsible for interacting with the Answer API.
/// </summary>
/// <param name="http"><c>HttpClient</c> instance used for sending requests.</param>
public class AnswerHttpClient(HttpClient http)
{
    /// <summary>
    /// Sends a request to create a new answer in the backend system.
    /// </summary>
    /// <param name="answer">The <see cref="AnswerRequest"/> containing the answer details to be submitted.</param>
    /// <returns>
    /// A <see cref="AnswerApiResponse"/> representing the created answer, or <c>null</c> if the request fails.
    /// </returns>
    public async Task<AnswerApiResponse?> CreateAnswerAsync(AnswerRequest answer)
    {
        try
        {
            var result = await http.PostAsJsonAsync(ApiRoutes.Answers.Create, answer);
            return await result.Content.ReadFromJsonAsync<AnswerApiResponse>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating answer: {e.Message}");
            return null;
        }
    }
}