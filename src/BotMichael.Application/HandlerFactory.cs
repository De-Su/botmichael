using BotMichael.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.Enums;

namespace BotMichael.Application;

public class HandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public HandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IHandler Create(UpdateType type)
    {
        var handlerType = type switch
        {
            UpdateType.Message => typeof(MessageHandler),
            UpdateType.CallbackQuery => typeof(CallbackQueryHandler),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        return _serviceProvider.GetServices<IHandler>().Single(x => x.GetType() == handlerType);
    }
}