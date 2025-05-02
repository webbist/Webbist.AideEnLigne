using AssistClub.Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssistClub.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents a repository for managing categories in the database.
/// </summary>
/// <param name="db">The database context.</param>
public class CategoryRepository(AssistClubDbContext db) : ICategoryRepository
{
    /// <summary>
    /// Retrieves all categories from the database, allowing further filtering, sorting, and pagination.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> representing the categories in the database.
    /// </returns>
    public async Task<IQueryable<Category>> GetCategoriesAsync()
    {
        return await Task.FromResult(db.Categories);
    }
    
    /// <summary>
    /// Retrieves a category by its unique identifier.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <returns>
    /// The <see cref="Category"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await db.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
    }
}