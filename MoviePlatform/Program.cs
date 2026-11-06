using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.Data.Interceptors;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(connectionString)
        .AddInterceptors(new AuditInterceptor()));

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();