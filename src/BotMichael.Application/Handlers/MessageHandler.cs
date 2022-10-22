using BotMichael.Domain.Event;
using LanguageExt;

namespace BotMichael.Application.Handlers;

/// <summary>
/// Обработчик сообщений для типа <see cref="Message"/>
/// </summary>
public class MessageHandler : IHandler
{
    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var user = update.Message!.Chat.Id;
        var lastEvent = Db.GetLastEvent(user);
        var request = GetRequestMessage(lastEvent, update.Message, user);
        var reply = request.CreateReply();
        var newEvent = lastEvent.CreateEventByReply(request, reply);
        Db.SaveEvent(newEvent);
        await SendReply(botClient, reply.Content, token);
    }

    private static RequestMessage GetRequestMessage(Option<BaseEvent> lastEvent, Message message, long userId) =>
        lastEvent
            .Match<RequestMessage>(
                ev =>
                    ev switch
                    {
                        StartEvent => new SetEmailRequest(message.Text!, userId),
                        SetEmailEvent => new SetPasswordRequest(message.Text!, userId),
                        SetPasswordEvent => new StartRequest(userId),
                        _ => throw new ArgumentOutOfRangeException(nameof(ev))
                    },
                () => new StartRequest(userId));

    private static Task SendReply(ITelegramBotClient botClient, BaseContent content, CancellationToken token) =>
        content switch
        {
            TextContent(var text, var userId) => botClient.SendTextMessageAsync(userId, text, cancellationToken: token),
            _ => Task.CompletedTask
        };
}