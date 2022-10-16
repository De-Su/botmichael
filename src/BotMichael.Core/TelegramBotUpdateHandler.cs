using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace BotMichael.Core;

public sealed class TelegramBotUpdateHandler : IUpdateHandler
{
    private readonly ILogger<TelegramBotUpdateHandler> _logger;

    public TelegramBotUpdateHandler(ILogger<TelegramBotUpdateHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling message...");
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        // Echo received message text
        await botClient.SendTextMessageAsync(chatId, messageText, cancellationToken: cancellationToken);
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        _logger.LogError(errorMessage);
        return Task.CompletedTask;
    }
}