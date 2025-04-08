using AssistClub.UI.Blazor.Components.Enums;
using AssistClub.UI.Blazor.Routing;
using AssistClub.UI.Blazor.Models;

namespace AssistClub.UI.Blazor.HttpClients;

/// <summary>
/// Represents the client responsible for interacting with the Question API.
/// </summary>
/// <param name="http"><c>HttpClient</c> instance used for sending requests.</param>
public class QuestionHttpClient(HttpClient http)
{
    /// <summary>
    /// Sends a request to create a new question in the system.
    /// </summary>
    /// <remarks>
    /// Allows users to submit questions through the UI, which are then stored in the backend for other users to view and respond to.
    /// </remarks>
    /// <param name="question">The <see cref="QuestionRequest"/> entity containing user input.</param>
    /// <returns>
    /// The created <see cref="QuestionApiResponse"/> entity if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<QuestionApiResponse?> CreateQuestionAsync(QuestionRequest question)
    {
        try
        {
            var result = await http.PostAsJsonAsync(QuestionApiRouting.CreateRoute, question);
            return await result.Content.ReadFromJsonAsync<QuestionApiResponse>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating question: {e.Message}");
            return null;
        }
    }

    /// <summary>
    /// Sends a request to retrieve all questions in descending order by creation date and filtered by visibility.
    /// </summary>
    /// <remarks>
    /// This method utilizes OData to allow dynamic filtering and sorting of questions.
    /// </remarks>
    /// <param name="visibility">The visibility filter for questions (<c>public</c> or <c>private</c>).</param>
    /// <returns>
    /// A collection of <see cref="QuestionApiResponse"/> entities if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<IEnumerable<QuestionApiResponse>?> GetQuestionsAsync(QuestionVisibility visibility)
    {
        try
        {
            var query = $"$orderby=CreatedAt desc&$filter=Visibility eq '{visibility}'";
            var url = $"{QuestionApiRouting.GetAllRoute}?{query}";
            var result = await http.GetFromJsonAsync<IEnumerable<QuestionApiResponse>>(url);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting questions: {e.Message}");
            return null;
        }
    }

    public async Task<QuestionApiResponse?> GetQuestionAsync(Guid id)
    {
        try
        {
            var result = await http.GetFromJsonAsync<QuestionApiResponse>($"{QuestionApiRouting.GetByIdRoute(id)}");
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting question: {e.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Sends a request to update an existing question in the system.
    /// </summary>
    /// <param name="id">The ID of the question to update.</param>
    /// <param name="updatedQuestion">The <see cref="QuestionRequest"/> entity containing updated question details.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the update was successful or not.
    /// </returns>
    public async Task<bool> UpdateQuestionAsync(Guid id, QuestionRequest updatedQuestion)
    {
        try
        {
            var result = await http.PutAsJsonAsync(QuestionApiRouting.UpdateRoute(id), updatedQuestion);
            return result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating question: {e.Message}");
            return false;
        }
    }
}