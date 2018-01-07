using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class HelloCommand : Command
    {
        public override string Name { get; set; } = "/hi";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            
            await client.SendTextMessageAsync(chatId, $"Привет! Меня зовут {BotSettings.Name}.\nВведи /help чтобы знать, что я умею)", replyToMessageId: messageId);
        }
    }
}
