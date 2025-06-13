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
    /// Represents the repository interface for managing question-related database operations.
    /// </summary>
    /// <remarks>
    /// Abstracts the data access layer to maintain separation of concerns 
    /// and prevent direct interaction between business logic and the database.
    /// </remarks>
    public interface IQuestionRepository
    {
        #region Methods
        /// <summary>
        /// Adds a new question to the database.
        /// </summary>
        /// <param name="question">The <see cref="Question"/> entity to add.</param>
        /// <returns>The <see cref="Question"/> entity that was added.</returns>
        Task<Question> CreateQuestionAsync(Question question);
    
        /// <summary>
        /// Retrieves a question by its unique identifier including the user who submitted it.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>The <see cref="Question"/> entity if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if multiple questions are found with the same ID.</exception>
        Task<Question?> GetQuestionByIdAsync(Guid id);
    
        /// <summary>
        /// Retrieves all questions from the database, allowing further filtering, sorting, and pagination.
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/> representing the questions in the database.</returns>
        Task<IQueryable<Question>> GetQuestions();
    
        /// <summary>
        /// Updates an existing question in the database.
        /// </summary>
        /// <param name="question">The updated <see cref="Question"/> entity.</param>
        /// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
        Task<bool> UpdateQuestionAsync(Question question);
    
        /// <summary>
        /// Deletes a question from the database.
        /// </summary>
        /// <param name="id">The ID of the question to delete.</param>
        /// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteQuestionAsync(Guid id);
        #endregion
    }
}