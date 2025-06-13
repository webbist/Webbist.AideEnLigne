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

using Microsoft.Extensions.DependencyInjection;

namespace Webbist.AideEnLigne.Model
{
    /// <summary>
    /// Represents the dependency injection configuration for the model layer.
    /// </summary>
    public static class DependencyInjection
    {
        #region Methods
        /// <summary>
        /// Adds the model services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>
        /// The updated service collection.
        /// </returns>
        public static IServiceCollection AddModel(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IAnswerVoteRepository, AnswerVoteRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
        #endregion
    }
}
