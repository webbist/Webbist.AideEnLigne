using AssistClub.UI.Blazor.Routing;

namespace AssistClub.UI.Blazor.HttpClients;

/// <summary>
/// Represents the client responsible for interacting with the File API.
/// </summary>
/// <param name="http"><c>HttpClient</c> instance used for sending requests.</param>
public class FileHttpClient(HttpClient http)
{
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