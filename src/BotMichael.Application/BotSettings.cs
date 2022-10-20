using Telegram.Bot.Types.Enums;

namespace BotMichael.Application;

public sealed class BotSettings
{
    public string Token { get; set; } = "invalid token";

    public static ReceiverOptions ReceiverOptions => new()
    {
        AllowedUpdates = new[]
        {
            UpdateType.Message,
            UpdateType.CallbackQuery
        }
    };
}