using AssistClub.UI.Blazor.Components.Enums;
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
    /// <param name="userId">The unique identifier of the user to check if he voted for any answer.</param>
    /// <returns>
    /// A collection of <see cref="AnswerApiResponse"/> entities if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<IEnumerable<AnswerApiResponse>?> GetAnswersAsync(Guid questionId, Guid userId)
    {
        try
        {
            var query = $"$orderby=CreatedAt asc&$filter=Question/Id eq {questionId}";
            var url = $"{AnswerApiRouting.GetAllRoute(userId)}?{query}";
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
    /// Sends a request to update the status of an existing answer in the backend system.
    /// </summary>
    /// <param name="answerId">The unique identifier of the answer to be updated.</param>
    /// <param name="newStatus">The new status to be set for the answer.</param>
    /// <returns>
    /// A boolean indicating whether the update was successful (<c>true</c> if successful, <c>false</c> otherwise).
    /// </returns>
    public async Task<bool> UpdateAnswerStatusAsync(Guid answerId, AnswerStatus newStatus)
    {
        try
        {
            var result = await http.PutAsJsonAsync(AnswerApiRouting.UpdateStatusRoute(answerId), newStatus);
            return result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating answer status: {e.Message}");
            return false;
        }
    }

    /// <summary>
    /// Sends a request to update an existing answer in the backend system.
    /// </summary>
    /// <param name="id">The unique identifier of the answer to be updated.</param>
    /// <param name="updatedAnswer">The <see cref="AnswerRequest"/> containing the updated answer details.</param>
    /// <returns>
    /// A boolean indicating whether the update was successful (<c>true</c> if successful, <c>false</c> otherwise).
    /// </returns>
    public async Task<bool> UpdateAnswerAsync(Guid id, AnswerRequest updatedAnswer)
    {
        try
        {
            var response = await http.PutAsJsonAsync(AnswerApiRouting.UpdateRoute(id), updatedAnswer);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating answer: {e.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Sends a request to retrieve all answers associated with a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose answers are being retrieved.</param>
    /// <returns>
    /// A collection of <see cref="AnswerApiResponse"/> entities if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<IEnumerable<AnswerApiResponse>?> GetAnswersByUserIdAsync(Guid userId)
    {
        try
        {
            var query = $"$orderby=CreatedAt desc&$filter=User/Id eq {userId}";
            var url = $"{AnswerApiRouting.GetAllRoute(userId)}?{query}";
            var result = await http.GetFromJsonAsync<IEnumerable<AnswerApiResponse>>(url);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting answers by user ID: {e.Message}");
            return null;
        }
    }

    /// <summary>
    /// Sends a request to delete an existing answer from the backend system.
    /// </summary>
    /// <param name="answerId">The unique identifier of the answer to be deleted.</param>
    /// <returns>
    /// A boolean indicating whether the deletion was successful (<c>true</c> if successful, <c>false</c> otherwise).
    /// </returns>
    public async Task<bool> DeleteAnswerAsync(Guid answerId)
    {
        try
        {
            var result = await http.DeleteAsync(AnswerApiRouting.DeleteRoute(answerId));
            return result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting answer: {e.Message}");
            return false;
        }
    }
}