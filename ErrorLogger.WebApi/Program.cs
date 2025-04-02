using ErrorLogger.Application;
using ErrorLogger.Infrastructure;
using ErrorLogger.WebApi.Middleware;
using ErrorLogger.Core.Interfaces;
using ErrorLogger.Infrastructure.Repositories;
using ErrorLogger.Infrastructure.Services;
using ErrorLogger.Application.Commands.LogError;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Reflection;
using Microsoft.OpenApi.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Додавання сервісів
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Налаштування Swagger
builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new OpenApiInfo 
   { 
       Title = "Error Logger API", 
       Version = "v1",
       Description = "API для реєстрації та управління системними помилками"
   });

   var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
   var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
   c.IncludeXmlComments(xmlPath);
});

// Реєстрація MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
   Assembly.GetExecutingAssembly(),
   Assembly.GetAssembly(typeof(LogErrorCommand))
));

// Реєстрація залежностей
builder.Services.AddScoped<IErrorRepository, ErrorRepository>();
builder.Services.AddScoped<INotificationService, TelegramNotificationService>();

// Налаштування CORS
builder.Services.AddCors(options =>
{
   options.AddDefaultPolicy(policy =>
   {
       policy.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
   });
});

// Додавання додаткових сервісів
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Налаштування логування
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Налаштування pipeline
if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseCors();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();