using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class BotCommand
    {
        // Название комманды для бота
        public string Command { get; set; }
        // Делегат который принимает 2 параметра и ничего не возвращает
        // Вызывать при выполнениее команды
        public Action<Update, long> Execute;
        // Делегат который принимает 2 параметра и ничего не возвращает
        // Вызывается при ошибке
        public Action<Update, long> OnError;        

    }
}
