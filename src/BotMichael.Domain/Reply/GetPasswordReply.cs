using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

public record GetPasswordReply(BaseContent Content) : ReplyMessage(Content)
{
}