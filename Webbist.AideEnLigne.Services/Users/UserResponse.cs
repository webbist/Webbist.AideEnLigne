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

namespace Webbist.AideEnLigne.Services.Users
{
    /// <summary>
    /// Represents the user data returned by the API.
    /// </summary>
    /// <remarks>
    /// Ensures that only relevant user details are exposed to the client.
    /// </remarks>
    public class UserResponse
    {
        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; }
    
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string Firstname { get; set; }
    
        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>  
        public string Lastname { get; set; }
    
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; }
    
        /// <summary>
        /// Gets or sets the URL of the user's profile photo, if available.
        /// </summary>
        public string? Photo { get; set; }
    
        /// <summary>
        /// Gets or sets the club to which the user is affiliated.
        /// </summary>
        public string Club { get; set; }
    
        /// <summary>
        /// Gets or sets the user's microsite URL.
        /// </summary>
        public string Microsite { get; set; }
    
        /// <summary>
        /// The user's role in the system (<c>admin</c> or <c>user</c>).
        /// </summary>
        public string Role { get; set; }
        #endregion
    }
}