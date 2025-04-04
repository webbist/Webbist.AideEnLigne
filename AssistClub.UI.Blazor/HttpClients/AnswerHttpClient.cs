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
            var result = await http.PostAsJsonAsync(AnswerApiRouting.CreateRoute, answer);
            return await result.Content.ReadFromJsonAsync<AnswerApiResponse>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating answer: {e.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Sends a request to retrieve all answers associated with a specific question.
    /// </summary>
    /// <remarks>
    /// This method utilizes OData to allow dynamic filtering and sorting of answers.
    /// </remarks>
    /// <param name="questionId">The unique identifier of the question for which answers are being retrieved.</param>
    /// <returns>
    /// A collection of <see cref="AnswerApiResponse"/> entities if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<IEnumerable<AnswerApiResponse>?> GetAnswersAsync(Guid questionId)
    {
        try
        {
            var query = $"$orderby=CreatedAt asc&$filter=QuestionId eq {questionId}";
            var url = $"{AnswerApiRouting.GetAllRoute}?{query}";
            var result = await http.GetFromJsonAsync<IEnumerable<AnswerApiResponse>>(url);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting answers: {e.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Sends a request to update an existing answer in the backend system.
    /// </summary>
    /// <param name="answerId">The unique identifier of the answer to be updated.</param>
    /// <param name="isOfficial">Indicates whether the answer should be marked as official or not.</param>
    /// <returns>
    /// A boolean indicating whether the update was successful.
    /// </returns>
    public async Task<bool> MarkAnswerAsOfficial(Guid answerId, bool isOfficial)
    {
        try
        {
            var result = await http.PutAsJsonAsync(AnswerApiRouting.UpdateOfficialStatusRoute(answerId), isOfficial);
            return result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error marking answer as official: {e.Message}");
            return false;
        }
    }
}