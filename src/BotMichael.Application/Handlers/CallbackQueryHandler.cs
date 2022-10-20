namespace BotMichael.Application.Handlers;

public class CallbackQueryHandler : IHandler
{
    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        /*
        1) get user state from db
        2) create reply from state
        3) create new state
        4) save new state
        5) send data to user
        */
        // var state = GetState(updateMessage.From);//inpure
        // var reply = CreateReply(update, state);
        // var newState = CreateNewState(user, reply);
        // SaveState(newState);//inpure
        // SendReply(reply); //inpure
        
        var callBackQuery = update.CallbackQuery!;
        var message = callBackQuery.Message!;
        var command = callBackQuery.Data;

        await botClient.SendTextMessageAsync(message.Chat.Id, $"Была выбрана команда {command}", cancellationToken: cancellationToken);
    }
}