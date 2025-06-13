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

using Webbist.AideEnLigne.Services.Questions;
using Webbist.AideEnLigne.Services.Users;

namespace Webbist.AideEnLigne.Services.Answers
{
    /// <summary>
    /// Represents the response structure for an answer.
    /// </summary>
    /// <remarks>
    /// Ensures that only relevant answer details are exposed to the client.
    /// </remarks>
    public class AnswerResponse
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the uploaded attachment file.
        /// </summary>
        public string? AttachmentName { get; set; }

        /// <summary>
        /// Gets or sets the full content of the answer.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the answer was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has voted for this answer.
        /// </summary>
        public bool HasVoted { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the answer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the question being answered.
        /// </summary>
        public QuestionResponse Question { get; set; }

        /// <summary>
        /// Gets or sets the status of the answer (<c>Pending</c>, <c>Approved</c>, or <c>Archived</c>).
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the answer was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user who created the answer.
        /// </summary>
        public UserResponse User { get; set; }

        /// <summary>
        /// Gets or sets the number of votes received for the answer.
        /// </summary>
        public int VoteCount { get; set; }
        #endregion
    }
}