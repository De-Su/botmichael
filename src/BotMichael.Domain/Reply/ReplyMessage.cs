using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

public abstract record ReplyMessage(BaseContent Content)
{
}