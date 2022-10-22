namespace BotMichael.Domain.Event;

/// <summary>
/// Событие начала работы
/// </summary>
public record StartEvent(Guid TopicId, long UserId) : BaseEvent(TopicId, UserId);