using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

/// <summary>
/// Ответ для получения пароля
/// </summary>
public record GetPasswordReply(BaseContent Content) : ReplyMessage(Content)
{
}