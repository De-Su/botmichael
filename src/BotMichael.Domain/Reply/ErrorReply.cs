using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

public record ErrorReply(BaseContent Content) : ReplyMessage(Content)
{
}