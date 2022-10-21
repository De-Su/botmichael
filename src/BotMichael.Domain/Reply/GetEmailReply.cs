using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

public record GetEmailReply(BaseContent Content) : ReplyMessage(Content)
{
}