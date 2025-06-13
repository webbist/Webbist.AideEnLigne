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

namespace Webbist.AideEnLigne.Services.Answers
{
    /// <summary>
    /// Represents the request structure for creating a new answer.
    /// </summary>
    /// <remarks>
    /// Ensures that only valid and necessary data is received from the client before processing.
    /// </remarks>
    public class AnswerRequest
    {
        #region Constants
        /// <summary>
        /// Maximum allowed length for the content.
        /// </summary>
        public const int ContentMaxLength = 2000;
        #endregion

        #region Methods
        /// <summary>
        /// Gets or sets the name of the uploaded attachment file.
        /// </summary>
        public string? AttachmentName { get; set; }

        /// <summary>
        /// Gets or sets the full content of the answer.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the question being answered.
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user submitting the answer.
        /// </summary>
        public Guid UserId { get; set; }
        #endregion
    }
}