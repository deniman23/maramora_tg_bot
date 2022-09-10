using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;

namespace Kropka_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("5792488144:AAGs37ZhLM7PnS7oyKiSUYO8kwky2ASHbYk");
            botClient.StartReceiving(Update, Error);
            Console.ReadLine();

        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message.Text != null)
            {
                //Отображение в консоли ника пользователя и его сообщения
                Console.WriteLine($"{message.Chat.FirstName}  |  {message.Text}");
                //При первом запуске будет выводить "Вітаю вас"
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вітаю вас!");
                    //Кнопка начала поиска
                    ReplyKeyboardMarkup keyboard = new(new[]
                    {
                        new KeyboardButton[] { "Знайсці суразмоўцу" }
                    })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Націсніце на кнопку, каб пачаць пошук", replyMarkup: keyboard);
                    return;
                }
                //Если нечего ответить, то отвечает "Вы сказалі: + что написано в сообщении
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Вы сказалі: \n{message.Text}");
                //Кнопка выбора того что ищем
                if (message.Text == "Знайсці суразмоўцу")
                {
                    InlineKeyboardMarkup keyboardMarkup = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Пошук суполкі")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Пошук ментара")
                        },
                    });
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Націсніце на кнопку, каб пачаць пошук", replyMarkup: keyboardMarkup);

                    if (message.Text == "Пошук суполкі")
                    {
                        InlineKeyboardMarkup keyboardMarkup1 = new(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Дызайн", "Фатаграфія")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Діджітал мастацтва")
                        },
                    });
                    }
                    if (message.Text == "Пошук ментара")
                    {
                        InlineKeyboardMarkup keyboardMarkup2 = new(new[]
                        {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Дызайн", "Фатаграфія")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Діджітал мастацтва")
                        },
                    });
                    }
                }

            }
        }
        
        //Error 
        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Памылка тэлеграм API:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}

