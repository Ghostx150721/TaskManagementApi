using FluentValidation.AspNetCore;
using TaskManagementApi.API.Middleware;
using TaskManagementApi.Application.Interfaces;
using TaskManagementApi.Application.Services;
using TaskManagementApi.Application.Validators;
using TaskManagementApi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers()
    .AddFluentValidation(cfg =>
        cfg.RegisterValidatorsFromAssemblyContaining<CreateTaskValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();