using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Polling;

namespace BotMichael.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IUpdateHandler, TelegramBotUpdateHandler>();
        return services;
    }
}