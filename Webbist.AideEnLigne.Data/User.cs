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
    /// Represents a user in the AideEnLigne application. 
    /// </summary>
    public partial class User
    {
        #region Properties
        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        public virtual ICollection<Answer> Answers { get; set; } = [];

        /// <summary>
        /// Gets or sets the answer votes.
        /// </summary>
        public virtual ICollection<AnswerVote> AnswerVotes { get; set; } = [];

        /// <summary>
        /// Gets or sets the club.
        /// </summary>
        public string Club { get; set; } = null!;

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string Firstname { get; set; } = null!;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string Lastname { get; set; } = null!;

        /// <summary>
        /// Gets or sets the microsite.
        /// </summary>
        public string Microsite { get; set; } = null!;

        /// <summary>
        /// Gets or sets the notification preference.
        /// </summary>
        public virtual NotificationPreference? NotificationPreference { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public string? Photo { get; set; }

        /// <summary>
        /// Gets or sets the questions.
        /// </summary>
        public virtual ICollection<Question> Questions { get; set; } = [];

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public string Role { get; set; } = null!;
        #endregion
    }
}
