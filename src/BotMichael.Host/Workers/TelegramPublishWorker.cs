namespace BotMichael.Host.Workers;

internal sealed class TelegramPublishWorker : BackgroundService
{
    private readonly ILogger<TelegramPublishWorker> _logger;
    private readonly ITelegramBotClient _client;

    public TelegramPublishWorker(ILogger<TelegramPublishWorker> logger, ITelegramBotClient client)
    {
        _logger = logger;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Start publishing...");
            await Publish();
            await Task.Delay(60_000, stoppingToken);
        }
    }

    private async Task Publish()
    {
        //todo: get aggregated state from db and push message to chat
        //_client.SendPhotoAsync()
        await Task.CompletedTask;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting publish worker...");
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping publish worker...");
        await base.StopAsync(cancellationToken);
    }
}