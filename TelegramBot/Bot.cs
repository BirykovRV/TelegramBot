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

        private static List<ICommand> commands = new List<ICommand>();

        public static List<ICommand> GetCommands
        {
            get
            {
                return commands;
            }
        }

        public Bot()
        {
            bot = new TelegramBotClient(BotSettings.Key);
            commands.Add(new HelloCommand());
            commands.Add(new HelpCommand());
        }        

        public async Task Run()
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
                            else
                            {
                                await bot.SendTextMessageAsync(update.Message.Chat.Id, $"Я такой команды не знаю(\nВведи /help чтобы знать, что я умею)");
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
