using BotMichael.Domain.Reply;

namespace BotMichael.Domain.State;

public record WaitingPasswordState : BaseState
{
    public override BaseState Next(ReplyMessage reply)
    {
        if (reply is ErrorReply)
        {
            return new WaitingPasswordState();
        }

        return new ReadyState();
    }
}