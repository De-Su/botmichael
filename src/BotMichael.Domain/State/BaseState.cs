using BotMichael.Domain.Reply;

namespace BotMichael.Domain.State;

//todo: хранить состояние
public abstract record BaseState
{
    public abstract BaseState Next(ReplyMessage reply);
}