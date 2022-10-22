namespace BotMichael.Domain.Event;

/// <summary>
/// Событие указания электронной почты
/// </summary>
/// <param name="Email">Электронная почта</param>
public record SetEmailEvent(string Email, Guid TopicId, long UserId) : BaseEvent(TopicId, UserId);