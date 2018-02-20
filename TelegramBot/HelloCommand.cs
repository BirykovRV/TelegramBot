using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class HelloCommand : ICommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name { get; set; } = "/hi";
        /// <summary>
        /// Вызывает команду
        /// </summary>
        /// <param name="message">принимает сообщение</param>
        /// <param name="client">Ссылка на экземпляр бота</param>
        public async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            
            await client.SendTextMessageAsync(chatId, $"Привет! Меня зовут {BotSettings.Name}.\nВведи /help чтобы знать, что я умею)", replyToMessageId: messageId);
        }
    }
}
