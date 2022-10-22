using BotMichael.Domain.Reply;

namespace BotMichael.Domain.Request;

/// <summary>
/// Базовое сообщение запроса
/// </summary>
/// <param name="UserId">Идентификатор пользователя</param>
public abstract record RequestMessage(long UserId)
{
    /// <summary>
    /// Сформировать ответ
    /// </summary>
    public abstract ReplyMessage CreateReply();
}