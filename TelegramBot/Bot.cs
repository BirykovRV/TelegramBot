using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot
{
    class Bot
    {
        private TelegramBotClient bot;
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
            commands.Add(new HelloCommand());
            commands.Add(new HelpCommand());
            commands.Add(new InlineButtonCommand());
        }

        private async void Bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var cbid = e.CallbackQuery.Id;
            var user = e.CallbackQuery.From.Username;
            var cid = e.CallbackQuery.Message.Chat.Id;
            var data = e.CallbackQuery.Data;
            var text = e.CallbackQuery.Message.Text;

            if (!string.IsNullOrEmpty(data))
            {
                switch (data)
                {
                    case "yes":
                        await bot.EditMessageTextAsync(cid, e.CallbackQuery.Message.MessageId, "Boo");
                        break;
                    case "no":
                        await bot.EditMessageTextAsync(cid, e.CallbackQuery.Message.MessageId, $"{data} is pressed");
                        break;
                    default:
                        break;
                }
            }
        }

        private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var cid = e.Message.Chat.Id;
            var txt = e.Message.Text;
            var user = e.Message.From.FirstName + " " + e.Message.From.LastName;
            Console.WriteLine($"{e.Message.From.Id} - {user} - {txt}");
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
