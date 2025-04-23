
using ErrorLogger.Infrastructure.Services;
using ErrorLogger.WebApi;


var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

// Розкоментуйте для генерації зашифрованих токенів
TokenEncryptionConsole.GenerateEncryptedTokensFromConfig(builder.Configuration);

var app = builder.Build();

// Перевірка конфігурації AutoMapper
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var mapper = services.GetRequiredService<AutoMapper.IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
        Console.WriteLine("AutoMapper налаштовано правильно");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка конфігурації AutoMapper: {ex.Message}");
    }
}

startup.Configure(app);

app.Run();