using System.Reflection;
using ErrorLogger.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorLogger.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            
            return services;
        }
    }
}