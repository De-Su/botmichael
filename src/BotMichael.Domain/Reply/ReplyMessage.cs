using BotMichael.Domain.Content;

namespace BotMichael.Domain.Reply;

/// <summary>
/// Сообщение-ответ
/// </summary>
/// <param name="Content">Контент сообщения</param>
public abstract record ReplyMessage(BaseContent Content)
{
}