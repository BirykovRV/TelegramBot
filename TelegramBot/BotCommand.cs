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
        // Число аргументов в команде
        public int CountArgs { get; set; }
        // Пример команды
        public string Example { get; set; }
        // Делегат который принимает 2 параметра и ничего не возвращает
        // Вызывать при выполнениее команды
        public Action<BotCommandModel, Update> Execute;
        // Делегат который принимает 2 параметра и ничего не возвращает
        // Вызывается при ошибке
        public Action<BotCommandModel, Update> OnError;

        /// <summary>
        /// Парсим полученную строку на наличие команд и аргументов
        /// </summary>
        /// <param name="text">Переданная строка</param>
        /// <returns>Возвращает объект BotCommandModel, если tru, иначе null</returns>
        public static BotCommandModel Parse(string text)
        {
            if (text.StartsWith("/"))
            {
                string[] split = text.Split(' ');
                string name = split?.FirstOrDefault();
                string[] args = split.Skip(1).Take(split.Count()).ToArray();

                return new BotCommandModel
                {
                    Command = name,
                    Args = args
                };
            }
            return null;
        }

    }
}
