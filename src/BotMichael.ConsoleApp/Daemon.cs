using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotMichael.ConsoleApp;

public class Daemon : IHostedService
{
    private readonly ILogger<Daemon> _logger;
    private readonly IConfiguration _configuration;

    public Daemon(ILogger<Daemon> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting bot...");
        
        var client = new TelegramBotClient(_configuration.GetRequiredSection("BotSettings").Get<BotSettings>().Token);
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
        };
        
        client.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cancellationToken);

        var me = await client.GetMeAsync(cancellationToken);
        _logger.LogInformation("Start listening for @{botName}", me.Username);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping bot...");
        return Task.CompletedTask;
    }
    
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling message");
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

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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