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

namespace Webbist.AideEnLigne.Services.Users
{
    /// <summary>
    /// Represents the user request data.
    /// </summary>
    public class UserRequest
    {
        #region Properties
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; }
    
        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string Firstname { get; set; }
    
        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string Lastname { get; set; }
    
        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string Email { get; set; }
    
        /// <summary>
        /// The URL of the user's profile photo, if available.
        /// </summary>
        public string? Photo { get; set; }
    
        /// <summary>
        /// The club to which the user is affiliated.
        /// </summary>
        public string Club { get; set; }
    
        /// <summary>
        /// The user's microsite URL.
        /// </summary>
        public string Microsite { get; set; }
    
        /// <summary>
        /// The user's role in the system (<c>admin</c> or <c>user</c>).
        /// </summary>
        public Role Role { get; set; }
        #endregion
    }
}