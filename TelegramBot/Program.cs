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
        static TelegramBotClient Bot;
        static int AdminId = 332021670;

        static List<BotCommand> Commands = new List<BotCommand>();

        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("533322105:AAHNbYcPCKNVWFSDvojXtrLmz_UYL6dkoFs");

            Commands.Add(new BotCommand
            {
                Command = "/help",
                Execute = async (update, chatId) =>
                {
                    await Bot.SendTextMessageAsync(chatId, "Список всех команд:\n" +
                        string.Join("\n", Commands.Select(s => s.Command)));
                },
                OnError = async (update, chatId) =>
                {
                    await Bot.SendTextMessageAsync(chatId, "Не правильная команда:\n/help");
                }
            });

            Commands.Add(new BotCommand
            {
                Command = "/help@Papoy_bot",
                Execute = async (update, chatId) =>
                {
                    await Bot.SendTextMessageAsync(chatId, "Список всех команд:\n" +
                        string.Join("\n", Commands.Select(s => s.Command)));
                },
                OnError = async (update, chatId) =>
                {
                    await Bot.SendTextMessageAsync(chatId, "Не правильная команда:\n/help@Papoy_bot");
                }
            });

            Test().Wait();

            Console.ReadKey();
        }

        static async Task Test()
        {
            int offset = 1;

            UpdateType[] type = { UpdateType.MessageUpdate, UpdateType.ChannelPost };

            while (true)
            {                
                var updates = await Bot.GetUpdatesAsync(offset ,100, 3, type);

                foreach (var update in updates)
                {
                    if (update.Message != null)
                    {
                        int senderId = update.Message.From.Id;
                        long chatId = update.Message.Chat.Id;
                        int cmdCouter = 1;
                        string text = update.Message?.Text;                        

                        foreach (var cmd in Commands)
                        {
                            if (cmd.Command == text)
                            {
                                cmd.Execute?.Invoke(update, chatId);
                                break;
                            }
                            else if (Commands.Count == cmdCouter)
                            {
                                cmd.OnError?.Invoke(update, chatId);
                            }
                            cmdCouter++;
                        }
                    }
                    offset = update.Id + 1;
                }
            }
        }
    }
}
