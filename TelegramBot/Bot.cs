using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot
{
    static class Bot
    {
        private static TelegramBotClient client;

        private static List<Command> commandList;

        public static IReadOnlyList<Command> Commands { get => commandList.AsReadOnly(); }

        public static TelegramBotClient Get()
        {
            if (client != null)
            {
                return client;
            }

            commandList = new List<Command>();
            commandList.Add( new HelloCommand());
            client = new TelegramBotClient(AppSettings.Token);
            
            return client;
        }
    }
}
