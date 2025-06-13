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
    /// Represents the service interface for handling user-related business logic.
    /// </summary>
    /// <remarks>
    /// Ensures that user data is processed correctly before being sent to the presentation layer, 
    /// maintaining security and consistency.
    /// </remarks>
    public interface IUserService
    {
        #region Methods
        /// <summary>
        /// Retrieves a user's profile information using their email address.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="UserResponse"/> containing user details if found; otherwise, <c>null</c>.
        /// </returns>
        Task<UserResponse?> GetUserByEmailAsync(string email);
    
        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="user">The user details to create.</param>
        /// <returns>A <see cref="UserResponse"/> containing the created user details.</returns>
        Task<UserResponse> CreateUserAsync(UserRequest user);
    
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="UserResponse"/> containing user details if found; otherwise, <c>null</c>.
        /// </returns>
        Task<UserResponse?> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Retrieves the notification preferences for a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose notification preferences to retrieve.</param>
        /// <returns>
        /// A <see cref="NotificationPreferenceRequest"/> containing the user's notification preferences if found; otherwise, <c>null</c>.
        /// </returns>
        Task<NotificationPreferenceRequest?> GetUserNotificationPreferencesAsync(Guid userId);
    
        /// <summary>
        /// Updates the notification preferences for a user.
        /// </summary>
        /// <param name="preferences">The <see cref="NotificationPreferenceRequest"/> containing the updated preferences.</param>
        /// <returns>
        /// Returns <c>true</c> if the update was successful; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> UpdateUserNotificationPreferencesAsync(NotificationPreferenceRequest preferences);
        #endregion
    }
}