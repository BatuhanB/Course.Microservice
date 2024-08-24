using Course.Invoice.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Invoice.Presentation;
public static class ServiceRegistration
{
    public static IServiceCollection AddPresentationDependencies(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddInfrastructureDependencies(configuration);
        return services;
    }
}
