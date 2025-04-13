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

    /// <summary>
    /// Sends a request to retrieve a specific question by its ID.
    /// </summary>
    /// <param name="id">The ID of the question to retrieve.</param>
    /// <returns>
    /// The <see cref="QuestionApiResponse"/> entity if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
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
    
    /// <summary>
    /// Sends a request to retrieve all questions created by a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user whose questions to retrieve.</param>
    /// <returns>
    /// A collection of <see cref="QuestionApiResponse"/> entities if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<IEnumerable<QuestionApiResponse>?> GetQuestionsByUserIdAsync(Guid userId)
    {
        try
        {
            var query = $"$orderby=CreatedAt desc&$filter=User/Id eq {userId}";
            var url = $"{QuestionApiRouting.GetAllRoute}?{query}";
            var result = await http.GetFromJsonAsync<IEnumerable<QuestionApiResponse>>(url);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting questions by user ID: {e.Message}");
            return null;
        }
    }

    /// <summary>
    /// Sends a request to delete an existing question from the system.
    /// </summary>
    /// <param name="id">The ID of the question to delete.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the deletion was successful or not.
    /// </returns>
    public async Task<bool> DeleteQuestionAsync(Guid id)
    {
        try
        {
            var result = await http.DeleteAsync(QuestionApiRouting.DeleteRoute(id));
            return result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting question: {e.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Sends a request to upload a file to the server.
    /// </summary>
    /// <param name="formDataContent">The <c>HttpContent</c> containing the file data.</param>
    /// <returns>
    /// The <c>HttpResponseMessage</c> indicating the result of the upload operation.
    /// </returns>
    public async Task<HttpResponseMessage> UploadFileAsync(HttpContent formDataContent)
    {
        try
        {
            var result = await http.PostAsync(FileApiRouting.UploadFileRoute, formDataContent);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error uploading file: {e.Message}");
            return new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Content = new StringContent(e.Message)
            };
        }
    }

    /// <summary>
    /// Sends a request to delete a file from the server.
    /// </summary>
    /// <param name="filename">The name of the file to delete.</param>
    /// <returns>
    /// A <c>bool</c> indicating whether the deletion was successful or not.
    /// </returns>
    public async Task<bool> DeleteFileAsync(string filename)
    {
        try
        {
            var result = await http.DeleteAsync(FileApiRouting.DeleteFileRoute(filename));
            return result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting file: {e.Message}");
            return false;
        }
    }
}