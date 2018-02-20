using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();

            Console.WriteLine("Бот запущен");

            bot.RunAsync().Wait();            
                        
            Console.ReadKey();
        }
    }
}
