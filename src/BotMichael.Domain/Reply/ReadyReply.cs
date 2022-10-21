using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

public record ReadyReply(BaseContent Content) : ReplyMessage(Content)
{
}