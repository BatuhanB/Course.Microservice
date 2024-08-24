using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Invoice.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,IConfiguration configuration)
    {
        return services;
    }
}
