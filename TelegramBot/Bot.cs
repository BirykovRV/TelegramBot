using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Bot
    {
        private TelegramBotClient bot;
        private static InlineKeyboardMarkup inline = new InlineKeyboardMarkup();
        public int Voit { get; private set; }
        /// <summary>
        /// Создание списка команд для бота
        /// </summary>
        private static List<ICommand> commands = new List<ICommand>();
        /// <summary>
        /// Получить список команд
        /// </summary>
        public static List<ICommand> GetCommands
        {
            get
            {
                return commands;
            }
        }
        /// <summary>
        /// Создание бот-клиента, инициализация новых команд
        /// </summary>
        public Bot()
        {
            bot = new TelegramBotClient(BotSettings.Key);
            bot.StartReceiving();
            bot.OnMessage += Bot_OnMessage;
            bot.OnCallbackQuery += Bot_OnCallbackQuery;
            bot.OnReceiveError += Bot_OnReceiveError;
            commands.Add(new HelloCommand());
            commands.Add(new HelpCommand());
            //commands.Add(new InlineButtonCommand());            
        }

        private void Bot_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        {
            Console.WriteLine("Received error: {0} - {1}", e.ApiRequestException.ErrorCode, e.ApiRequestException.Message);
        }

        private async void Bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var cbid = e.CallbackQuery.Id;
            var user = e.CallbackQuery.From.Username;
            var cid = e.CallbackQuery.Message.Chat.Id;
            var mid = e.CallbackQuery.Message.MessageId;
            var data = e.CallbackQuery.Data;
            var text = e.CallbackQuery.Message.Text;
            var chat = e.CallbackQuery.ChatInstance;

            if (!string.IsNullOrEmpty(data))
            {
                if (data == "yes")
                {
                    Voit++;
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new [] // first row
                        {
                            InlineKeyboardButton.WithCallbackData("1.1"),
                            InlineKeyboardButton.WithCallbackData("1.2"),
                        },
                        new [] // second row
                        {
                            InlineKeyboardButton.WithCallbackData("2.1"),
                            InlineKeyboardButton.WithCallbackData("2.2"),
                        }
});
                    await bot.AnswerCallbackQueryAsync(cbid, "Ваш голос принят!");
                    await bot.EditMessageTextAsync(cid, mid, $"Всего голосов:\n{Voit}");
                    await bot.EditMessageReplyMarkupAsync(
                        chatId: cid,
                        messageId: mid,
                        replyMarkup: inlineKeyboard
                        );
                    //await bot.EditMessageTextAsync(
                    //    chatId: cid,
                    //    messageId: mid,
                    //    text: text,
                    //    replyMarkup: inline
                    //    );
                }
                else if (data == "no")
                {
                    Voit--;
                    await bot.AnswerCallbackQueryAsync(cbid, "Ваш голос удален!");
                }
                
                //switch (data)
                //{
                //    case "yes":
                //        await bot.EditMessageTextAsync(cid, mid, $"{text}");
                //        break;
                //    case "no":
                //        await bot.EditMessageTextAsync(cid, mid, $"{data} is pressed");
                //        break;
                //    default:
                //        break;
                //}
            }
        }

        private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
            {
                var cid = e.Message.Chat.Id;
                var txt = e.Message.Text;
                var user = e.Message.From.FirstName + " " + e.Message.From.LastName;
                Console.WriteLine($"{e.Message.Date} - {e.Message.From.Id} - {user} - {txt} - {Voit}");
                inline.InlineKeyboard = new InlineKeyboardButton[][]
                    {
                        new[]
                        {
                            new InlineKeyboardCallbackButton("Yes", "yes"),
                            new InlineKeyboardCallbackButton("No", "no")
                        }
                    };
                if (txt.Contains("/show"))
                {        
                    bot.SendTextMessageAsync(
                        chatId: cid,
                        text: $"Выберите ответ:",
                        replyMarkup: inline
                        );
                }

                foreach (var cmd in commands)
                {
                    if (txt.Contains(cmd.Name))
                    {
                        cmd.Execute(e.Message, bot);
                        break;
                    }
                }
            }
        }
    }
}
