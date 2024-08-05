using Course.Gateway.DelegateHandlers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<TokenExchangeDelegateHandler>();

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
{
    options.Authority = builder.Configuration["Token:Issuer"];
    options.Audience = builder.Configuration["Token:Audience"];
    options.MapInboundClaims = false;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // This is important for allowing cookies to be sent
        });
});


builder.Host.ConfigureAppConfiguration((hostingContext,config) =>
{
    config
    .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json")
    .AddEnvironmentVariables();
});

builder.Services.AddOcelot()
    .AddDelegatingHandler<TokenExchangeDelegateHandler>();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

await app.UseOcelot();

app.Run();
