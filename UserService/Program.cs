using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using UserService.CQRS.AuthService;
using UserService.Data;
using UserService.Entities;
using UserService.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(connectionString).AddInterceptors(new AuditInterceptor()));


builder.Services.AddSingleton<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ILogin, Login>();
// Controllers
builder.Services.AddControllers();
//gRPC
builder.Services.AddGrpc();
builder.Services.AddMagicOnion();
// OpenAPI
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new();

        document.Components.SecuritySchemes ??=
            new Dictionary<string, Microsoft.OpenApi.IOpenApiSecurityScheme>();

        document.Components.SecuritySchemes["Bearer"] =
            new Microsoft.OpenApi.OpenApiSecurityScheme
            {
                Type = Microsoft.OpenApi.SecuritySchemeType.Http,
                Scheme = "bearer"
            };

        return Task.CompletedTask;
    });
});
//Mediatr

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); });

var jwtKey = builder.Configuration["Jwt:SecretKey"]
             ?? throw new InvalidOperationException("JWT SecretKey is missing");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    await scope.ServiceProvider.GetRequiredService<UserDbContext>()
        .Database.MigrateAsync();
}


app.UseMiddleware<ExceptionHandlingMiddleware>();

// OpenAPI and Scalar

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapMagicOnionService();
app.Run();