using Course.Catalog.Service.Api;
using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Services.Category;
using Course.Catalog.Service.Api.Services.Course;
using Course.Catalog.Service.Api.Services.Generic;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Token:Issuer"];
        options.Audience = builder.Configuration["Token:Audience"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero, // Remove clock skew
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError("Authentication failed: {0}", context.Exception.Message);
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                if (!context.Response.HasStarted)
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError("Token is unauthorized: {0}", context.ErrorDescription);
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:URL"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:UserName"]!);
            host.Password(builder.Configuration["RabbitMQ:Password"]!);
        });
        cfg.ConfigureEndpoints(context);
    });
});


builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

await MigrateDataIfNotExists(app,CancellationToken.None);

static async Task<IServiceScope> MigrateDataIfNotExists(WebApplication app, CancellationToken cancellationToken)
{
    var scope = app.Services.CreateScope();

    var courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();
    var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();

    var courses = await courseService.GetAllAsync(cancellationToken);
    var categories = await categoryService.GetAllAsync(cancellationToken);

    if (!categories.Data.Any())
    {
        var categoryRes = categoryService.CreateAsync(new Category()
        {
            Name = "Category 1"
        }, cancellationToken).Result;

        if (categoryRes.IsSuccessful)
        {
            if (!courses.Data.Any())
            {
                await courseService.CreateAsync(new Course.Catalog.Service.Api.Models.Course
                {
                    Description = "Course 1 descriptin",
                    Name = "Course 1",
                    CategoryId = categoryRes.Data.Id,
                    CreatedDate = DateTime.Now,
                    Feature = new Feature() { Duration = 129, },
                    Image = "",
                    Price = 122,
                    UserId = "b27560fb-8385-4524-b052-faddced6a12d"
                }, cancellationToken);
            }
        }
    }

    return scope;
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
