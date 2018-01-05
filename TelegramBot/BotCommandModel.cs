using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    /// <summary>
    /// Определяет модель команды
    /// </summary>
    class BotCommandModel
    {
        // Комманда для бота
        public string Command { get; set; }
        // Колличество аргументов в комманде
        public string[] Args { get; set; }
    }
}
