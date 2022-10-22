using BotMichael.Domain.Reply;
using BotMichael.Domain.Request;
using LanguageExt;

namespace BotMichael.Domain.Event;

public static class Module
{
    public static Option<BaseEvent> CreateEventByReply(
        this Option<BaseEvent> lastEvent,
        RequestMessage request,
        ReplyMessage reply)
        =>
            lastEvent
                .Match(
                    x => reply switch
                    {
                        ErrorReply => Option<BaseEvent>.None,
                        _ => request switch
                        {
                            StartRequest => new StartEvent(Guid.NewGuid(), reply.Content.UserId),
                            SetEmailRequest r => new SetEmailEvent(r.Email, x.TopicId, x.UserId),
                            SetPasswordRequest r => new SetPasswordEvent(r.Password, x.TopicId, x.UserId),
                            _ => throw new ArgumentOutOfRangeException(nameof(request))
                        }
                    }
                    ,
                    () => new StartEvent(Guid.NewGuid(), reply.Content.UserId));
}