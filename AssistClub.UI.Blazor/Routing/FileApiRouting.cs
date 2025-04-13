namespace AssistClub.UI.Blazor.Routing;

/// <summary>
/// Defines the API endpoints used by the application for file-related operations.
/// </summary>
public class FileApiRouting
{
    /// <summary>
    /// API version used for routing.
    /// </summary>
    private const string Version = "v1";
    
    /// <summary>
    /// Base route for file operations.
    /// </summary>
    private const string Base = $"{Version}/files";
    
    /// <summary>
    /// Endpoint for uploading a file.
    /// </summary>
    public const string UploadFileRoute = $"{Base}/upload";
    
    /// <summary>
    /// Endpoint for downloading a file.
    /// </summary>
    public const string DownloadFileRoute = $"{Base}/download";
    
    /// <summary>
    /// Endpoint for deleting a file.
    /// </summary>
    public static string DeleteFileRoute(string filename) => $"{Base}/delete/{filename}";
}