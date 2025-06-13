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
    /// Represents the repository interface for managing answer-related database operations.
    /// </summary>
    /// <remarks>
    /// Abstracts the data access layer to maintain separation of concerns
    /// and prevent direct interaction between business logic and the database.
    /// </remarks>
    public interface IAnswerRepository
    {
        #region Methods
        /// <summary>
        /// Adds a new answer to the database and updates the status of the associated question.
        /// </summary>
        /// <param name="answer">The <see cref="Answer"/> entity to add.</param>
        /// <returns>The <see cref="Answer"/> entity that was added.</returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
        Task<Answer> CreateAnswerAsync(Answer answer);
    
        /// <summary>
        /// Retrieves all answers from the database, allowing further filtering, sorting, and pagination.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{T}"/> representing the answers in the database.
        /// </returns>
        Task<IQueryable<Answer>> GetAnswers();

        /// <summary>
        /// Updates the status of an answer and the associated question status.
        /// </summary>
        /// <param name="answerId">The unique identifier of the answer to be updated.</param>
        /// <param name="newStatus">The new status to be set for the answer.</param>
        /// <returns>
        /// Returns the updated <see cref="Answer"/> entity if successful; otherwise, <c>null</c>.
        /// </returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
        Task<Answer?> UpdateAnswerStatusAsync(Guid answerId, AnswerStatus newStatus);
    
        /// <summary>
        /// Updates an existing answer in the database.
        /// </summary>
        /// <param name="updatedAnswer">The <see cref="Answer"/> entity to update.</param>
        /// <returns>
        /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving the answer to the database.</exception>
        Task<bool> UpdateAnswerAsync(Answer updatedAnswer);

        /// <summary>
        /// Deletes an answer from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the answer to be deleted.</param>
        /// <returns>
        /// Returns <c>true</c> if the deletion was successful; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> DeleteAnswerAsync(Guid id);
        #endregion
    }
}