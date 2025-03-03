using Domain.Entities;

namespace AssistClub.UI.Blazor.HttpClients;

/// <summary>
/// HTTP client for interacting with the Question API.
/// This class is responsible for sending requests to the backend API 
/// and handling responses related to question management.
/// </summary>
/// <param name="http">The HttpClient instance to use for sending requests.</param>

public class QuestionHttpClient(HttpClient http)
{
    /// <summary>
    /// Sends a request to create a new question in the system.
    /// This method allows users to submit questions through the UI, which are then 
    /// stored in the backend for other users to view and respond to.
    /// </summary>
    /// <param name="question">The question entity containing user input.</param>
    /// <returns>
    /// The created <see cref="Question"/> entity if successful; otherwise, null in case of an error.
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