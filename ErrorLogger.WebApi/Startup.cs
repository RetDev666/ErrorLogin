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
            
            services.AddAutoMapper(config => 
            {
                config.AddProfile<ErrorMapperProfile>();
                config.AddProfile<ApiMappingProfile>();
                config.AddProfile<InfrastructureMappingProfile>();
            });
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(LogErrorCommand))
            ));
            
            services.AddSingleton<IErrorRepository, InMemoryErrorRepository>();
            services.AddScoped<INotificationService, TelegramNotificationService>();
            
            services.AddDataProtection();
            services.AddScoped<SecureTokenService>();
            
            services.AddLogging();
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
                    c.EnableDeepLinking();
                    c.DisplayRequestDuration();
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