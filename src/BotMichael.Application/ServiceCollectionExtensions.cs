using BotMichael.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace BotMichael.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTelegramApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IUpdateHandler, TelegramBotUpdateHandler>();
            
        //todo: reflection
        serviceCollection.AddSingleton<IHandler, MessageHandler>();
        serviceCollection.AddSingleton<IHandler, CallbackQueryHandler>();
        serviceCollection.AddSingleton<HandlerFactory>();
        return serviceCollection;
    }
    
}