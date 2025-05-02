using Domain.Entities;

namespace AssistClub.Application.Interfaces;

/// <summary>
/// Represents the repository interface for managing category-related database operations.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Retrieves all categories from the database, allowing further filtering, sorting, and pagination.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> representing the categories in the database.
    /// </returns>
    Task<IQueryable<Category>> GetCategoriesAsync();
    
    /// <summary>
    /// Retrieves a category by its unique identifier.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <returns>
    /// The <see cref="Category"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    Task<Category?> GetCategoryByNameAsync(string name);
}