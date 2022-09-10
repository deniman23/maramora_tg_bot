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
                //Кнопка выбора того что ищем
                if (message.Text == "Знайсці суразмоўцу")
                {
                    
                    ReplyKeyboardMarkup keyboardMarkup = new(new[]
                    {
                        new KeyboardButton[] { "Пошук суполкі", "Пошук ментара" }
                    })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вылучыце тое што вам трэба", replyMarkup: keyboardMarkup);
                    return;
                }
                // Если пользователь ищет беседы с другим пользователем то идет сюда и здесь выбирает вид деятолькости и после получает ссылку на пользователя по тематике
                if (message.Text == "Пошук суполкі")
                {
                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                    {
                        InlineKeyboardButton.WithUrl(
                        text: "Дызайн",
                        url: "https://t.me/danetolk"
                        ),
                        InlineKeyboardButton.WithUrl(
                        text: "Фатаграфія",
                        url: "https://t.me/da_kondratenko"
                        ),
                        InlineKeyboardButton.WithUrl(
                        text: "Діджітал мастацтва",
                        url: "https://t.me/krvkristina"
                        ),
                    }
                    );
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Выберыце від дзейнасці і прыемных зносін", replyMarkup: inlineKeyboard);
                    return;
                }
                //Аналогичная логика, но тут ищут ментора
                if (message.Text == "Пошук ментара")
                {
                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                    {
                        InlineKeyboardButton.WithUrl(
                        text: "Дызайн",
                        url: "https://t.me/danetolk"
                        ),
                        InlineKeyboardButton.WithUrl(
                        text: "Фатаграфія",
                        url: "https://t.me/da_kondratenko"
                        ),
                        InlineKeyboardButton.WithUrl(
                        text: "Діджітал мастацтва",
                        url: "https://t.me/krvkristina"
                        ),
                    }
                    );
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Выберыце від дзейнасці і прыемных зносін", replyMarkup: inlineKeyboard);
                    return;
                }
                //Если нечего ответить, то отвечает "Вы сказалі: + что написано в сообщении
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Вы сказалі: \n{message.Text}");
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

