using BotMichael.Domain.Content;
using BotMichael.Domain.Reply;

namespace BotMichael.Domain.Request;

/// <summary>
/// Запрос на указание пароля
/// </summary>
/// <param name="Password">Пароль</param>
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