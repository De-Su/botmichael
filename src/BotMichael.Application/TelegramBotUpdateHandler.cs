namespace BotMichael.Application;

public sealed class TelegramBotUpdateHandler : IUpdateHandler
{
    private readonly ILogger<TelegramBotUpdateHandler> _logger;
    private readonly HandlerFactory _factory;

    public TelegramBotUpdateHandler(ILogger<TelegramBotUpdateHandler> logger, HandlerFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }
    
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (BotSettings.ReceiverOptions!.AllowedUpdates!.All(x => x != update.Type))
        {
            _logger.LogError("Невозможно обработать сообщения {id} типа {type}", update.Id, update.Type.ToString());
            return;
        }
        
        _logger.LogInformation("Обработка сообщения {id}", update.Id);
        await _factory.Create(update.Type).Handle(botClient, update, cancellationToken);
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