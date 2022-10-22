namespace BotMichael.Domain.Content;

/// <summary>
/// Базовый контент
/// </summary>
/// <param name="UserId">Идентификатор пользователя</param>
public abstract record BaseContent(long UserId)
{
}