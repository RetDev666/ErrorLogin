using ErrorLogger.Domain.Interfaces;
using ErrorLogger.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace ErrorLogger.Infrastructure.Services
{
    public class TelegramNotificationService : INotificationService
    {
        private readonly ILogger<TelegramNotificationService> logger;
        private readonly TelegramBotClient botClient;
        private readonly string chatId;

        public TelegramNotificationService(
            IConfiguration configuration,
            ILogger<TelegramNotificationService> logger)
        {
            this.logger = logger;

            var botToken = configuration["Telegram:BotToken"];
            chatId = configuration["Telegram:ChatId"];

            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(chatId))
            {
                this.logger.LogError("Відсутня конфігурація Telegram-бота");
                throw new ArgumentException("Telegram-бот не сконфігурований. Перевірте appsettings.json");
            }

            botClient = new TelegramBotClient(botToken);
            this.logger.LogInformation("TelegramNotificationService ініціалізовано з ID чату: {ChatId}", chatId);
        }

        public async Task<bool> SendErrorNotificationAsync(Error error, CancellationToken cancellationToken = default)
        {
            try
            {
                var message = FormatErrorMessage(error);
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: message,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    cancellationToken: cancellationToken
                );
                
                logger.LogInformation("Повідомлення про помилку успішно відправлено в Telegram: {ErrorId}", error.Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка відправки повідомлення в Telegram: {ErrorMessage}", ex.Message);
                return false;
            }
        }

        private string FormatErrorMessage(Error error)
        {
            return $"⚠️ *ПОМИЛКА*\n" +
                   $"\n*ID:* {error.Id}" +
                   $"\n*Час:* {error.Timestamp:yyyy-MM-dd HH:mm:ss}" +
                   $"\n*Код статусу:* {error.StatusCode}" +
                   $"\n*Джерело:* {error.Source}" +
                   $"\n*Повідомлення:* {error.Message}" +
                   (string.IsNullOrEmpty(error.StackTrace) ? "" : $"\n\n*Стек виклику:*\n```\n{error.StackTrace}\n```");
        }
    }
}