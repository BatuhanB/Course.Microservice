using Course.Invoice.Application.Abstractions.Data;
using Course.Invoice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Invoice.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(conf =>
        {
            conf.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddSingleton<IApplicationDbContext,ApplicationDbContext>();
        return services;
    }
}
