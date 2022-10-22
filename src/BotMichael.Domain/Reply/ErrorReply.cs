using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

/// <summary>
/// Ответ-ошибка
/// </summary>
public record ErrorReply(BaseContent Content) : ReplyMessage(Content)
{
}