using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

/// <summary>
/// Ответ для получения пароля
/// </summary>
public record GetEmailReply(BaseContent Content) : ReplyMessage(Content)
{
}