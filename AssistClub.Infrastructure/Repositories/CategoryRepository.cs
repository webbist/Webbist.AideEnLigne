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

using AssistClub.Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Webbist.AideEnLigne.Data.Repositories
{
    /// <summary>
    /// Represents a repository for managing categories in the database.
    /// </summary>
    /// <param name="db">The database context.</param>
    public class CategoryRepository(DataContext db) : ICategoryRepository
    {
        #region Methods
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
        #endregion
    }
}