using Domain.Entities;

namespace AssistClub.UI.Blazor.HttpClients;

public class QuestionHttpClient(HttpClient http)
{
    public async Task<bool> CreateQuestionAsync(Question question)
    {
        try
        {
            var result = await http.PostAsJsonAsync("v1/question/create", question);
            //return await result.Content.ReadFromJsonAsync<Question>();
            return result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating question: {e.Message}");
            //return null;
            return false;
        }
    }
}