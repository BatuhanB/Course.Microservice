using Course.Discount.Service.Api.Service.Concretes;
using Course.Discount.Service.Api.Service.Contracts;
using Course.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnection>((sp) => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

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
    });

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await MigrateDatabaseIfNotExists(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

static async Task<IServiceScope> MigrateDatabaseIfNotExists(WebApplication app){
    var scope = app.Services.CreateScope();
    var discountService = scope.ServiceProvider.GetRequiredService<IDiscountService>();

    await discountService.CreateDbIfNotExists();
    var discounts = await discountService.GetAllAsync();
    if(!discounts.Data.Any()){
        await discountService.CreateAsync(new Course.Discount.Service.Api.Models.Discount(){
            Code = "Aa78hdJsul34kLM",
            Rate = 10,
            UserId = "b27560fb-8385-4524-b052-faddced6a12d"
        });
    }

    return scope;
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
