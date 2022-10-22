using BotMichael.Domain.Event;
using LanguageExt;

namespace BotMichael.Application;

public static class Db
{
    private static readonly Dictionary<long, List<BaseEvent>> _events = new();

    public static Option<BaseEvent> GetLastEvent(long userId) =>
        _events.TryGetValue(userId, out var result)
            ? result.Any()
                ? result.Last()
                : Option<BaseEvent>.None
            : Option<BaseEvent>.None;

    public static bool SaveEvent(Option<BaseEvent> @event) =>
        @event.Match(x =>
            {
                if (_events.TryGetValue(x.UserId, out var result))
                {
                    result.Add(x);
                }
                else
                {
                    _events[x.UserId] = new List<BaseEvent> {x};
                }

                return true;
            },
            () => false);
}