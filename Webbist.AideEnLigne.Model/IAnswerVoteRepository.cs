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
    /// Represents the repository interface for managing answer votes in the database.
    /// </summary>
    public interface IAnswerVoteRepository
    {
        #region Methods
        /// <summary>
        /// Retrieves a vote for a specific answer by a user.
        /// </summary>
        /// <param name="userId">The ID of the user who voted.</param>
        /// <param name="answerId">The ID of the answer that was voted on.</param>
        /// <returns>
        /// An <see cref="AnswerVote"/> object representing the vote, or <c>null</c> if no vote was found.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if multiple votes are found for the same user and answer.
        /// </exception>
        Task<AnswerVote?> GetVoteAsync(Guid userId, Guid answerId);
    
        /// <summary>
        /// Adds a new vote to the database.
        /// </summary>
        /// <param name="vote">The <see cref="AnswerVote"/> entity to add.</param>
        /// <returns>
        /// A <c>bool</c> indicating whether the vote was successfully added.
        /// </returns>
        /// <exception cref="DbUpdateException">
        /// Thrown if an error occurs while saving the vote to the database.
        /// </exception>
        Task<bool> AddVoteAsync(AnswerVote vote);
    
        /// <summary>
        /// Removes a vote from the database.
        /// </summary>
        /// <param name="vote">The <see cref="AnswerVote"/> entity to remove.</param>
        /// <returns>
        /// A <c>bool</c> indicating whether the vote was successfully removed.
        /// </returns>
        /// <exception cref="DbUpdateException">
        /// Thrown if an error occurs while saving the vote to the database.
        /// </exception>
        Task<bool> RemoveVoteAsync(AnswerVote vote);
        #endregion
    }
}