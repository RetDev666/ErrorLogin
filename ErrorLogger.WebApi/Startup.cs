using Microsoft.OpenApi.Models;
using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Infrastructure.Services;
using ErrorLogger.WebApi.Middleware;
using System.Reflection;
using ErrorLogger.Application.Commands;
using ErrorLogger.Application.Mappings;
using ErrorLogger.Infrastructure.Mappings;
using ErrorLogger.WebApi.Mappings;
using ErrorLogger.Infrastructure.Persistence;

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
            // Базові сервіси
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Swagger
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

            // AutoMapper - реєструємо тільки один раз!
            services.AddAutoMapper(config => 
            {
                config.AddProfile<ErrorMapperProfile>();
                config.AddProfile<ApiMappingProfile>();
                config.AddProfile<InfrastructureMappingProfile>();
            });
            
            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(LogErrorCommand))
            ));

            // Репозиторії та сервіси
            // Змінено з ErrorRepository на InMemoryErrorRepository 
            services.AddSingleton<IErrorRepository, InMemoryErrorRepository>();
            services.AddScoped<INotificationService, TelegramNotificationService>();
            
            // DataProtection і безпека
            services.AddDataProtection();
            services.AddScoped<SecureTokenService>();
            
            // Додавання логування
            services.AddLogging();
        }

        public void Configure(WebApplication app)
        {
            // Налаштування для розробки
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Error Logger API V1");
                    c.EnableDeepLinking();
                    c.DisplayRequestDuration();
                });
            }

            // CORS повинен бути перед маршрутизацією
            app.UseCors();
            
            // Обробка помилок
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Базова маршрутизація
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            
            app.MapControllers();
        }
    }
}