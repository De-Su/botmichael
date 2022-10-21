using BotMichael.Domain.Reply;

namespace BotMichael.Domain.State;

public record InitialState : BaseState
{
    public override BaseState Next(ReplyMessage reply)
    {
        if (reply is ErrorReply)
        {
            return new InitialState();
        }

        return new WaitingEmailState();
    }
}