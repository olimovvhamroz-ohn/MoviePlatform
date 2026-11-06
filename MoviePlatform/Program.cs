using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.Data.Interceptors;
using MoviePlatform.Mapping;
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


var app = builder.Build();

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