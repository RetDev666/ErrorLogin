using ErrorLogger.WebApi;
using ErrorLogger.Infrastructure.Tools;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

// Розкоментуйте для генерації зашифрованих токенів
TokenEncryptionConsole.GenerateEncryptedTokensFromConfig(builder.Configuration);

var app = builder.Build();

startup.Configure(app);

app.Run();