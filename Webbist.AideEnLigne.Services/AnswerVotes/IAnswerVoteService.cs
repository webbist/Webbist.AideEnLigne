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

namespace Webbist.AideEnLigne.Services.AnswerVotes
{
    /// <summary>
    /// Represents the service interface for handling business logic related to answer votes.
    /// </summary>
    public interface IAnswerVoteService
    {
        #region Methods
        /// <summary>
        /// Toggles the vote for a specific answer by a user.
        /// </summary>
        /// <param name="request">The request containing the user ID and answer ID.</param>
        /// <returns>
        /// A <c>bool</c> indicating whether the vote was successfully toggled.
        /// </returns>
        Task<bool> ToggleVoteAsync(AnswerVoteRequest request);
        #endregion
    }
}