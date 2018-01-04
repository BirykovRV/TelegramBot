using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class Program
    {
        static TelegramBotClient Bot;
        static int AdminId = 332021670;
        static List<BotCommand> Commands = new List<BotCommand>();

        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("533322105:AAHNbYcPCKNVWFSDvojXtrLmz_UYL6dkoFs");

            Commands.Add(new BotCommand
            {
                Command = "/help",
                Example = "/help",
                CountArgs = 0,
                Execute = async (model, update) =>
                {
                    await Bot.SendTextMessageAsync(update.Message.From.Id, $"Список всех команд:\n" +
                        string.Join("\n", Commands.Select(s => s.Example)));
                },
                OnError = async (model, update) =>
                {
                    if (model.Args.Length != 0)
                    {
                        await Bot.SendTextMessageAsync(update.Message.From.Id, $"Не верное кол-во аргумантов.\nИспользуйте команду так /help");
                    }
                }
            });

            //Commands.Add(new BotCommand
            //{
            //    Command = "/help",
            //    Example = "/help",
            //    CountArgs = 0,
            //    Execute = async (model, update) =>
            //    {
            //        await Bot.SendTextMessageAsync(update.Message.From.Id, $"Список всех команд:\n" +
            //            string.Join("\n", Commands.Select(s => s.Example)));
            //    },
            //    OnError = async (model, update) =>
            //    {
            //        if (model.Args.Length != 0)
            //        {
            //            await Bot.SendTextMessageAsync(update.Message.From.Id, $"Не верное кол-во аргумантов.\nИспользуйте команду так /help");
            //        }
            //    }
            //});

            RunAsync().Wait();

            Console.ReadKey();
        }

        static async Task RunAsync()
        {
            await Bot.SendTextMessageAsync(AdminId, "Бот запущен");
            var offset = 0;
            while (true)
            {
                var updates = await Bot.GetUpdatesAsync(offset);

                foreach (var update in updates)
                {
                    //if (update.Message.From.Id == AdminId)
                    //{
                    if (update.Message.Type == MessageType.TextMessage && offset == 0)
                    {                        
                        await Bot.SendChatActionAsync(update.Message.From.Id, ChatAction.Typing);
                        Task.Delay(1000).Wait();
                        await Bot.SendTextMessageAsync(update.Message.From.Id, $"Привет! Я бот Papoy. Меня создал Роман Бирюков. \nДля получения списка всех команд введи /help.");
                    }
                    else if (update.Message.Type == MessageType.TextMessage)
                    {
                        string text = update.Message.Text;
                        var model = BotCommand.Parse(text);
                        if (model != null)
                        {
                            foreach (var cmd in Commands)
                            {
                                if (cmd.Command == model.Command && cmd.CountArgs == model.Args.Length)
                                {
                                    cmd.Execute?.Invoke(model, update);
                                }
                                else
                                {
                                    cmd.OnError?.Invoke(model, update);
                                }
                            }
                        }
                        else
                        {
                            await Bot.SendChatActionAsync(update.Message.From.Id, ChatAction.Typing);
                            Task.Delay(1000).Wait();
                            await Bot.SendTextMessageAsync(update.Message.From.Id, "Это не команда.\nДля просмотра списка команд введи /help");
                        }                        
                    }
                    //}
                    offset = update.Id + 1;
                }
                Task.Delay(500).Wait();
            }
        }
    }
}
