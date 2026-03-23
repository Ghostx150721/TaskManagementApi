using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.API.Middleware;
using TaskManagementApi.Application.Interfaces;
using TaskManagementApi.Application.Services;
using TaskManagementApi.Application.Validators;
using TaskManagementApi.Infrastructure.Persistence;
using Polly;
using Polly.Extensions.Http;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // 🔥 THIS IS THE FIX
});

// Services
builder.Services.AddControllers()
    .AddFluentValidation(cfg =>
        cfg.RegisterValidatorsFromAssemblyContaining<CreateTaskValidator>());

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorCodesToAdd: null
            );
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddHealthChecks();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var retries = 5;

    while (retries > 0)
    {
        try
        {
            db.Database.Migrate();
            Console.WriteLine("Database ready!");
            break;
        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"Retrying DB... Attempts left: {retries}");
            Thread.Sleep(3000);

            if (retries == 0)
                throw;
        }
    }
}


// Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

// test run