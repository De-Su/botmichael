namespace BotMichael.Domain.Content;

public record TextContent(string Text, long UserId) : BaseContent(UserId)
{
}