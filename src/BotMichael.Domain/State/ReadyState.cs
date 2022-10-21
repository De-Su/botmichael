using BotMichael.Domain.Reply;

namespace BotMichael.Domain.State;

public record ReadyState : BaseState
{
    public override BaseState Next(ReplyMessage reply)
    {
        return new ReadyState();
    }
}