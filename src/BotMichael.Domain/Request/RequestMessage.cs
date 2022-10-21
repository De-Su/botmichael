using BotMichael.Domain.Reply;

namespace BotMichael.Domain.Request;

public abstract record RequestMessage(long UserId)
{
    public abstract ReplyMessage CreateReply();
}