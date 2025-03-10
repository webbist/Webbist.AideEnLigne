using Domain.Entities;

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
    /// <param name="question">The <see cref="Question"/> entity containing user input.</param>
    /// <returns>
    /// The created <see cref="Question"/> entity if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<Question?> CreateQuestionAsync(Question question)
    {
        try
        {
            var result = await http.PostAsJsonAsync("v1/question/create", question);
            return await result.Content.ReadFromJsonAsync<Question>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating question: {e.Message}");
            return null;
        }
    }
}