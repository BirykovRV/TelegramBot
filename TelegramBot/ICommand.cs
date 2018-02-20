using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    interface ICommand
    {
        string Name { get; set; }

        void Execute(Message message, TelegramBotClient client);
    }
}
