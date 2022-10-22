namespace BotMichael.Domain.Content;

/// <summary>
/// Текстовый контент
/// </summary>
/// <param name="Text">Текст</param>
public record TextContent(string Text, long UserId) : BaseContent(UserId)
{
}