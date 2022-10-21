using BotMichael.Domain.Reply;

namespace BotMichael.Domain.State;

public record WaitingEmailState : BaseState
{
    public override BaseState Next(ReplyMessage reply)
    {
        if (reply is ErrorReply)
        {
            return new WaitingEmailState();
        }

        return new WaitingPasswordState();
    }
}