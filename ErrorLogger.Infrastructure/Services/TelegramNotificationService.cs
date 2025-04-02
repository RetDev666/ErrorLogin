using System;
using System.Threading;
using System.Threading.Tasks;
using ErrorLogger.Core.Interfaces;
using ErrorLogger.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace ErrorLogger.Infrastructure.Services
{
    public class TelegramNotificationService : INotificationService
    {
        private readonly ILogger<TelegramNotificationService> _logger;
        private readonly TelegramBotClient _botClient;
        private readonly string _chatId;

        public TelegramNotificationService(
            IConfiguration configuration,
            ILogger<TelegramNotificationService> logger)
        {
            _logger = logger;

            var botToken = configuration["Telegram:BotToken"];
            _chatId = configuration["Telegram:ChatId"];

            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(_chatId))
            {
                _logger.LogError("Відсутня конфігурація Telegram-бота");
                throw new ArgumentException("Telegram-бот не сконфігурований. Перевірте appsettings.json");
            }

            _botClient = new TelegramBotClient(botToken);
            _logger.LogInformation("TelegramNotificationService ініціалізовано з ID чату: {ChatId}", _chatId);
        }

        public async Task<bool> SendErrorNotificationAsync(Error error, CancellationToken cancellationToken = default)
        {
            try
            {
                var message = FormatErrorMessage(error);
                await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: message,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    cancellationToken: cancellationToken
                );
                
                _logger.LogInformation("Повідомлення про помилку успішно відправлено в Telegram: {ErrorId}", error.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка відправки повідомлення в Telegram: {ErrorMessage}", ex.Message);
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