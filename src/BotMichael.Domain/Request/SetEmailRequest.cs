using BotMichael.Domain.Content;
using BotMichael.Domain.Reply;

namespace BotMichael.Domain.Request;

public record SetEmailRequest(string Email, long UserId) : RequestMessage(UserId)
{
    public override ReplyMessage CreateReply()
    {
        //todo: нужная валидация
        if (Email != "hello@privet.ru")
        {
            return new ErrorReply(new TextContent("Почта неправильная", UserId));
        }
        return new GetPasswordReply(new TextContent("Код отправил на почту. Укажи его сюда, как придет", UserId));
    }
}