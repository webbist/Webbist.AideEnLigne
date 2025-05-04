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
    /// Sends a request to retrieve questions filtered by visibility, search term, and date range.
    /// </summary>
    /// <param name="visibility">The visibility filter for questions (<c>public</c> or <c>private</c>).</param>
    /// <param name="search">The search term to filter questions by title or content.</param>
    /// <param name="from">The start date for filtering questions.</param>
    /// <param name="to">The end date for filtering questions.</param>
    /// <returns>
    /// A collection of <see cref="QuestionApiResponse"/> entities if successful; otherwise, <c>null</c> in case of an error.
    /// </returns>
    public async Task<IEnumerable<QuestionApiResponse>?> GetQuestionsFilteredAsync(
        QuestionVisibility visibility,
        string? search = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        try
        {
            var filters = new List<string>
            {
                $"Visibility eq '{visibility}'"
            };

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                filters.Add($"(contains(tolower(Title),'{lowerSearch}') or contains(tolower(Content),'{lowerSearch}'))");
            }

            if (from.HasValue)
            {
                var fromDate = from.Value.ToString("yyyy-MM-dd");
                filters.Add($"CreatedAt ge {fromDate}");
            }

            if (to.HasValue)
            {
                var toDate = to.Value.ToString("yyyy-MM-dd");
                filters.Add($"CreatedAt le {toDate}");
            }

            var filterQuery = string.Join(" and ", filters);
            var url = $"{QuestionApiRouting.GetAllRoute}?$orderby=CreatedAt desc&$filter={filterQuery}";
            var result = await http.GetFromJsonAsync<IEnumerable<QuestionApiResponse>>(url);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting filtered questions: {e.Message}");
            return null;
        }
    }
}