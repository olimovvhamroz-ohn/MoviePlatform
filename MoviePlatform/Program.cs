using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.Data.Interceptors;
using MoviePlatform.Mapping;
using MoviePlatform.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(connectionString)
        .AddInterceptors(new AuditInterceptor()));

// Controllers
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddOpenApi();

//AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MovieMappingProfile));

//Mediatr

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// OpenAPI and Scalar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();