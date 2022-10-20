namespace BotMichael.Application.Handlers;

public interface IHandler
{
    Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}