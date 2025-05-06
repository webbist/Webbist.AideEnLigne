namespace AssistClub.UI.Blazor.Models;

/// <summary>
/// Represents the response model for a category from the API.
/// </summary>
public class CategoryApiResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; }
}