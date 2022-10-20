using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotMichael.Application.Handlers;

public class MessageHandler : IHandler
{
    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // var state = GetState(updateMessage.From);//inpure
        // var reply = CreateReply(update, state);
        // var newState = CreateNewState(user, reply);
        // SaveState(newState);//inpure
        // SendReply(reply); //inpure
        
        var message = update.Message!;
        
        // Бот типа набирает текст 😁
        await Task.Delay(500, cancellationToken);
        await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing, cancellationToken);

        if (message.Text is { })
        {
            if (message.Text!.Equals("/start", StringComparison.OrdinalIgnoreCase))
            {
                var buttons = Enumerable.Range(1, 2).Select(x =>
                    InlineKeyboardButton.WithCallbackData(x.ToString(), $"command{x}"));

                var inlineKeyboard = new InlineKeyboardMarkup(buttons);

                await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Здарова, жми на кнопки",
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken);
                return;
            }
            
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Отвечаю: {message.Text}", cancellationToken: cancellationToken);
        }
    }
}