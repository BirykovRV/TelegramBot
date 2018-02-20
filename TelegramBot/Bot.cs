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
            commands.Add(new HelloCommand());
            commands.Add(new HelpCommand());
        }        
        /// <summary>
        /// Запуск бота
        /// </summary>
        /// <returns></returns>
        public async Task RunAsync()
        {
            var offset = 1;
            while (true)
            {
                var updates = await bot.GetUpdatesAsync(offset);

                foreach (var update in updates)
                {
                    if (update.Message != null)
                    {
                        foreach (var command in commands)
                        {
                            if (update.Message.Text != null && update.Message.Text.Contains(command.Name))
                            {
                                command.Execute(update.Message, bot);
                                break;
                            }
                        }
                    }
                    offset = update.Id + 1;
                }
            }
        }
    }
}
