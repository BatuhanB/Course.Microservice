using Course.Catalog.Service.Api.Services.Category;
using Course.Catalog.Service.Api.Services.Course;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Catalog.Service.Api.Settings;
using Microsoft.Extensions.Options;

namespace Course.Catalog.Service.Api;

public static class DependencyResolver
{
    public static IServiceCollection RegisterServices(this IServiceCollection services,IConfiguration configuration)
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
        services.AddSingleton<IDatabaseSettings>(opt => opt.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddAutoMapper(assembly);
        return services;
    }
}