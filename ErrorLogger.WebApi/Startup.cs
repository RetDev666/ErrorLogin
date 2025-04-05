using Microsoft.AspNetCore.DataProtection;
using Microsoft.OpenApi.Models;
using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Commands;
using ErrorLogger.Infrastructure.Repositories;
using ErrorLogger.Infrastructure.Services;
using ErrorLogger.WebApi.Middleware;
using System.Reflection;

namespace ErrorLogger.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Додайте DataProtection
            services.AddDataProtection();
    
            // Реєстрація служби шифрування
            services.AddScoped<SecureTokenService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
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

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(LogErrorCommand))
            ));

            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<INotificationService, TelegramNotificationService>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Error Logger API V1");
                });
            }

            app.UseCors();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}