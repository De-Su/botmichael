using BotMichael.Domain.Content;
using BotMichael.Domain.Reply;

namespace BotMichael.Domain.Request;

/// <summary>
/// Запрос на указание электронной почты
/// </summary>
/// <param name="Email">Электронная почта</param>
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