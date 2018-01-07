using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class HelpCommand : Command
    {
        public override string Name { get; set; } = "/help";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            long chatId = message.Chat.Id;
            int senderId = message.MessageId;

            await client.SendTextMessageAsync(chatId, "Список всех команд:\n" + string.Join("\n", Bot.GetCommands.Select(cmd => cmd.Name)), replyToMessageId: senderId);
        }
    }
}
