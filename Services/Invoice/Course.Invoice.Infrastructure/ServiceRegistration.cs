using Course.Invoice.Application.Abstractions.Data;
using Course.Invoice.Infrastructure.Data;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Course.Invoice.Infrastructure.Services;
using Course.Invoice.Application.Abstractions.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Course.Invoice.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(conf =>
        {
            conf.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IPdfConverterService, PdfConverterService>();
        services.AddSingleton<IFileService>(provider =>
        {
            var env = provider.GetRequiredService<IWebHostEnvironment>();
            var logger = provider.GetRequiredService<ILogger<FileService>>();
            return new FileService(env.WebRootPath, configuration, logger);
        });
        return services;
    }
}