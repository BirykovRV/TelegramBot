using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class InlineButtonCommand : ICommand
    {
        public string Name { get; set; } = "/show";

        public async void Execute(Message message, TelegramBotClient client)
        {
            var cid = message.Chat.Id;

            await client.SendTextMessageAsync(
                chatId: cid,
                text: "Вы нажали Да",
                replyMarkup: new InlineKeyboardMarkup(
                    new InlineKeyboardButton[][]
                        {
                            new[]
                            {
                                new InlineKeyboardCallbackButton($"Да", "yes"),
                                new InlineKeyboardCallbackButton("Нет", "no")
                            },
                            new[]
                            {
                                new InlineKeyboardCallbackButton("Да", "yes"),
                                new InlineKeyboardCallbackButton("Нет", "no")
                            }
                        }
                    )
                );
        }
    }
}
