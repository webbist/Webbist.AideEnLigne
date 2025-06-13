// Copyright (C) 2025 Webbist
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, see <https://www.gnu.org/licenses/old-licenses/gpl-2.0.html>.

using Webbist.AideEnLigne.Data;

namespace Webbist.AideEnLigne.Model
{
    /// <summary>
    /// Represents the repository interface for managing category-related database operations.
    /// </summary>
    public interface ICategoryRepository
    {
        #region Methods
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
        #endregion
    }
}