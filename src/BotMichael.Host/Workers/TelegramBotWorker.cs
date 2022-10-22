namespace BotMichael.Host.Workers;

/// <summary>
/// Worker для прослушивания сообщений, отправленных в бот
/// </summary>
internal sealed class TelegramBotWorker : BackgroundService
{
    private readonly ILogger<TelegramBotWorker> _logger;
    private readonly ITelegramBotClient _client;
    private readonly IUpdateHandler _handler;

    public TelegramBotWorker(ILogger<TelegramBotWorker> logger, ITelegramBotClient client, IUpdateHandler handler)
    {
        _logger = logger;
        _handler = handler;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.StartReceiving(_handler, BotSettings.ReceiverOptions, stoppingToken);

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