using BotMichael.Domain.Content;
using BotMichael.Domain.Reply;

namespace BotMichael.Domain.Request;

public record SetPasswordRequest(string Password, long UserId) : RequestMessage(UserId)
{
    public override ReplyMessage CreateReply()
    {
        //todo: нужная валидация
        if (Password != "123")
        {
            return new ErrorReply(new TextContent("Пароль неправильный", UserId));
        }
        return new ReadyReply(new TextContent("Все ок", UserId));
    }
}