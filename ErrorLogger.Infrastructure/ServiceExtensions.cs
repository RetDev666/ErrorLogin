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
            // Реєстрація репозиторію помилок
            services.AddSingleton<IErrorRepository, InMemoryErrorRepository>();
            
            // Реєстрація сервісу для відправлення повідомлень у Telegram
            services.AddSingleton<INotificationService, TelegramNotificationService>();
            
            return services;
        }
    }
}