namespace BotMichael.Domain.Event;

/// <summary>
/// Событие указания пароля
/// </summary>
/// <param name="Password">Пароль</param>
public record SetPasswordEvent(string Password, Guid TopicId, long UserId) : BaseEvent(TopicId, UserId);