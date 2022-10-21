﻿using BotMichael.Domain.Content;
using BotMichael.Domain.Reply;

namespace BotMichael.Domain.Request;

public record StartRequest(long UserId) : RequestMessage(UserId)
{
    public override ReplyMessage CreateReply()
    {
        return new GetEmailReply(new TextContent("Отправь почту", UserId));
    }
}