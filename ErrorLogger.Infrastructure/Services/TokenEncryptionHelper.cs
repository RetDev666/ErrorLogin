using Microsoft.Extensions.Configuration;
using ErrorLogger.Infrastructure.Tools;

namespace ErrorLogger.Infrastructure.Services
{
    public static class TokenEncryptionConsole
    {
        public static void GenerateEncryptedTokensFromConfig(IConfiguration configuration)
        {
            var botToken = configuration["Telegram:BotToken"];
            var chatId = configuration["Telegram:ChatId"];

            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(chatId))
            {
                Console.WriteLine("Токен або Chat ID не знайдено в конфігурації.");
                return;
            }

            var encryptedBotToken = TokenEncryptionHelper.GenerateEncryptedToken(botToken);
            var encryptedChatId = TokenEncryptionHelper.GenerateEncryptedToken(chatId);

            Console.WriteLine($"Encrypted Bot Token: {encryptedBotToken}");
            Console.WriteLine($"Encrypted Chat ID: {encryptedChatId}");
        }
    }
}