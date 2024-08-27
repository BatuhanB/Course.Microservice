using Model = Course.Notification.Service.Api.Models;
using Course.Notification.Service.Api.Consumers;
using Course.Notification.Service.Api.Hubs;
using Course.Notification.Service.Api.Services.Abstracts;
using Course.Notification.Service.Api.Services.Concretes;
using Course.Notification.Service.Api.Settings;
using Course.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

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
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/notificationHub")))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedNotificationEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:URL"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:UserName"]!);
            host.Password(builder.Configuration["RabbitMQ:Password"]!);
        });
        cfg.ReceiveEndpoint("created-order-notificationq", e =>
        {
            e.ConfigureConsumer<OrderCreatedNotificationEventConsumer>(context);
        });
    });
});

builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redisService = new RedisService(settings.Host, settings.Port);
    redisService.Connect();
    return redisService;
});

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapHub<NotificationHub>("/notificationHub");
});

// Delete all notifications for a user
app.MapDelete("/notification/d/{userId}", async (string userId, INotificationService notificationService) =>
{
    var response = await notificationService.Delete(userId);
    return response.Data ? Results.Ok(response) : Results.BadRequest(response);
});

// Get a specific notification by its ID for a user
app.MapGet("/notification/g/{userId}/{notificationId}", async (string userId, string notificationId, INotificationService notificationService) =>
{
    var response = await notificationService.Get(userId, notificationId);
    return response.Data != null ? Results.Ok(response) : Results.NotFound(response);
});

// Get all notifications for a user
app.MapGet("/notifications/g/{userId}", async (string userId, INotificationService notificationService) =>
{
    var response = await notificationService.GetAll(userId);
    return response.Data != null ? Results.Ok(response) : Results.NotFound(response);
});

// Get all notifications for a user
app.MapGet("/notifications/go/{userId}/{latestId}", async (string userId, string latestId, INotificationService notificationService) =>
{
    var response = await notificationService.GetAllCursorPagination(userId, latestId);
    return response.Data != null ? Results.Ok(response) : Results.NotFound(response);
});

// Set mark all as read notifications for a user
app.MapGet("/notifications/m/{userId}", async (string userId, INotificationService notificationService) =>
{
    var response = await notificationService.MarkAllAsRead(userId);
    return response.Data ? Results.Ok(response) : Results.NotFound(response);
});

// Add or update a single notification
app.MapPost("/notification/u", async ([FromBody] Model.NotificationDto notification, INotificationService notificationService) =>
{
    var notificationModel = new Model.Notification(
        notification.Title,
        notification.Description,
        notification.UserId
    );
    var response = await notificationService.Save(notificationModel);
    return response.Data ? Results.Ok(response) : Results.BadRequest(response);
});

app.Run();