namespace BotMichael.Domain.Event;

/// <summary>
/// Базовое событие
/// </summary>
/// <param name="TopicId">Уникальный идентификатор топика</param>
/// <param name="UserId">Идентификатор пользователя</param>
public abstract record BaseEvent(Guid TopicId, long UserId);