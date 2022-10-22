namespace BotMichael.Application.Handlers;

/// <summary>
/// Обработчик сообщений для типа <see cref="CallbackQuery"/>
/// </summary>
public class CallbackQueryHandler : IHandler
{
    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var callBackQuery = update.CallbackQuery!;
        var message = callBackQuery.Message!;
        var command = callBackQuery.Data;

        await botClient.SendTextMessageAsync(message.Chat.Id, $"Была выбрана команда {command}", cancellationToken: token);
    }
}