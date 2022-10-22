using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

/// <summary>
/// Ответ готовности
/// </summary>
public record ReadyReply(BaseContent Content) : ReplyMessage(Content)
{
}