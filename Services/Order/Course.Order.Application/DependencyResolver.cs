using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Course.Order.Application;
public static class DependencyResolver
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
    
        services.AddAutoMapper(assembly);

        return services;
    }
}