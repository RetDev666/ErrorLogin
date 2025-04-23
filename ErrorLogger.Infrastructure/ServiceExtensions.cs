using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Infrastructure.Persistence;
using ErrorLogger.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorLogger.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IErrorRepository, InMemoryErrorRepository>();
            
            services.AddSingleton<INotificationService, TelegramNotificationService>();
            
            return services;
        }
    }
}