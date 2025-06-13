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
    /// Represents a request to vote on an answer.
    /// </summary>
    public class AnswerVoteRequest
    {
        #region Properties
        /// <summary>
        /// The unique identifier of the answer being voted on.
        /// </summary>
        public Guid AnswerId { get; set; }

        /// <summary>
        /// The unique identifier of the user casting the vote.
        /// </summary>
        public Guid UserId { get; set; }
        #endregion
    }
}