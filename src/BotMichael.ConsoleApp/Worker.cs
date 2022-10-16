using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace BotMichael.ConsoleApp;

internal sealed class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITelegramBotClient _client;
    private readonly IUpdateHandler _handler;

    public Worker(ILogger<Worker> logger, IOptions<BotSettings> options, IUpdateHandler handler)
    {
        _logger = logger;
        _handler = handler;
        _client = new TelegramBotClient(options.Value.Token);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.StartReceiving(
            _handler,
            new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            },
            stoppingToken);

        var me = await _client.GetMeAsync(stoppingToken);
        _logger.LogInformation("Start listening for @{botName}", me.Username);
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting bot...");
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping bot...");
        await base.StopAsync(cancellationToken);
    }
}