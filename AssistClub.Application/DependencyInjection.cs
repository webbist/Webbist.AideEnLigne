using Microsoft.Extensions.DependencyInjection;

namespace AssistClub.Application;

/// <summary>
/// Dependency injection for the application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //add possible automapper configurations
        return services;
    }
}