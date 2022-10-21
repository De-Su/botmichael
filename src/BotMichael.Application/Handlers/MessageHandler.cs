namespace BotMichael.Application.Handlers;

public class MessageHandler : IHandler
{
    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message!;
        var user = message.Chat.Id;
        
        var state = Db.GetState(user); //inpure
        
        var requestMessage = GetRequestMessage(state, message, user); //pure
        var reply = requestMessage.CreateReply(); //pure
        var newState = state.Next(reply); //pure
        
        Db.SaveState(user, newState); //inpure
        await SendReply(botClient, reply, cancellationToken); //inpure
    }

    private async Task SendReply(ITelegramBotClient botClient, ReplyMessage reply, CancellationToken cancellationToken)
    {
        var sendText = (BaseContent content, CancellationToken token) => 
            botClient.SendTextMessageAsync(content.UserId, ((TextContent)reply.Content).Text, cancellationToken: token);
        
        var task = reply switch
        {
            GetEmailReply => sendText(reply.Content, cancellationToken),
            GetPasswordReply => sendText(reply.Content, cancellationToken),
            ReadyReply => sendText(reply.Content, cancellationToken),
            ErrorReply => sendText(reply.Content, cancellationToken),
            _ => Task.CompletedTask
        };

        await task;
    }

    private RequestMessage GetRequestMessage(BaseState baseState, Message message, long userId)
    {
        if (message.Text is { })
        {
            return baseState switch
            {
                InitialState => new StartRequest(userId),
                WaitingEmailState => new SetEmailRequest(message.Text, userId),
                WaitingPasswordState => new SetPasswordRequest(message.Text, userId),
                ReadyState => new StartRequest(userId),
                _ => throw new ArgumentOutOfRangeException(nameof(baseState)),
            };
        }

        return new StartRequest(userId);
    }
}