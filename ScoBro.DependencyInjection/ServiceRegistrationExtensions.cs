using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ScoBro.DependencyInjection;
public static class ServiceRegistrationExtensions {
    public static IServiceCollection RegisterServicesFromAssemblyContaining<T>(this IServiceCollection services) {
        return services.RegisterServices(typeof(T).Assembly);
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services, params Assembly[] assemblies) {
        services.Scan(selector => selector
           .FromAssemblies(assemblies)
           .AddClasses(classSelector => classSelector.AssignableTo<IScopedService>())
           .AsMatchingInterface()
           .WithScopedLifetime());

        services.Scan(selector => selector
           .FromAssemblies(assemblies)
           .AddClasses(classSelector => classSelector.AssignableTo<ITransientService>())
           .AsMatchingInterface()
           .WithTransientLifetime());

        services.Scan(selector => selector
           .FromAssemblies(assemblies)
           .AddClasses(classSelector => classSelector.AssignableTo<ISingletonService>())
           .AsMatchingInterface()
           .WithSingletonLifetime());

        return services;
    }
}
