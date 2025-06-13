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

namespace Webbist.AideEnLigne.Services.Answers
{
    /// <summary>
    /// Represents the service interface for handling business logic related to answers.
    /// </summary>
    /// <remarks>
    /// Ensures that answers are validated, processed, and formatted
    /// correctly before being stored in the database or returned to the client.
    /// Acts as a bridge between the repository and the API layer.
    /// </remarks>
    public interface IAnswerService
    {
        #region Methods
        /// <summary>
        /// Creates a new answer in the system.
        /// </summary>
        /// <remarks>
        /// Validates the input and delegates the persistence to the repository.
        /// Returns a simplified view model for external use.
        /// </remarks>
        /// <param name="answerRequest">The <see cref="AnswerRequest"/> containing answer details.</param>
        /// <returns>A <see cref="AnswerResponse"/> representing the created answer.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the answer content exceeds <see cref="AnswerRequest.ContentMaxLength"/> characters.
        /// </exception>
        Task<AnswerResponse> CreateAnswerAsync(AnswerRequest answerRequest);

        /// <summary>
        /// Retrieves all answers in the system, including user information.
        /// </summary>
        /// <param name="userId">The ID of the user to check if he voted for any answer.</param>
        /// <returns>
        /// An <see cref="IQueryable{T}"/> of <see cref="AnswerResponse"/> containing the answers.
        /// </returns>
        Task<IQueryable<AnswerResponse>> GetAnswersAsync(Guid userId);

        /// <summary>
        /// Updates the status of an answer and the associated question status.
        /// </summary>
        /// <param name="id">The unique identifier of the answer to be updated.</param>
        /// <param name="newStatus">The new status to be set for the answer.</param>
        /// <returns>
        /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> UpdateAnswerStatusAsync(Guid id, AnswerStatus newStatus);

        /// <summary>
        /// Updates an existing answer in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the answer to be updated.</param>
        /// <param name="answerRequest">The <see cref="AnswerRequest"/> containing the updated answer details.</param>
        /// <returns>
        /// A boolean indicating whether the update was successful (<c>true</c> if successful, <c>false</c> otherwise).
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the answer content exceeds <see cref="AnswerRequest.ContentMaxLength"/> characters.
        /// </exception>
        Task<bool> UpdateAnswerAsync(Guid id, AnswerRequest answerRequest);

        /// <summary>
        /// Deletes an answer from the system.
        /// </summary>
        /// <param name="answerId">The ID of the answer to be deleted.</param>
        /// <returns>
        /// A boolean indicating whether the deletion was successful (<c>true</c> if successful, <c>false</c> otherwise).
        /// </returns>
        Task<bool> DeleteAnswerAsync(Guid answerId);
        #endregion
    }
}