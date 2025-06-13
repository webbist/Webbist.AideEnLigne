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

namespace Webbist.AideEnLigne.Data
{
    /// <summary>
    /// Represents an answer to a question in the AideEnLigne application.
    /// </summary>
    public partial class AnswerVote
    {
        #region Properties
        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        public virtual Answer Answer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the answer identifier.
        /// </summary>
        public Guid AnswerId { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; } = null!;
        
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }
        #endregion
    }
}
