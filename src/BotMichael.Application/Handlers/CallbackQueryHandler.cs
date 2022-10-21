namespace BotMichael.Application.Handlers;

public class CallbackQueryHandler : IHandler
{
    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var callBackQuery = update.CallbackQuery!;
        var message = callBackQuery.Message!;
        var command = callBackQuery.Data;

        await botClient.SendTextMessageAsync(message.Chat.Id, $"Была выбрана команда {command}", cancellationToken: cancellationToken);
    }
}