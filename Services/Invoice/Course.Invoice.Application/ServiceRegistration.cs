using Course.Invoice.Application.Abstractions.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace Course.Invoice.Application;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services,IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(assembly);
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
