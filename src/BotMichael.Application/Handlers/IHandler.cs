namespace BotMichael.Application.Handlers;

/// <summary>
/// Обработчик сообщений <see cref="Update"/>
/// </summary>
public interface IHandler
{
    /// <summary>
    /// Обработать сообщений
    /// </summary>
    /// <param name="botClient">Клиент telegrambot</param>
    /// <param name="update">Сообщение</param>
    /// <param name="token">Токен отмены</param>
    Task Handle(ITelegramBotClient botClient, Update update, CancellationToken token);
}