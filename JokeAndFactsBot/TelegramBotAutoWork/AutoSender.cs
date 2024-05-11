using Telegram.Bot;
using TelegramGptBot;

namespace TelegramTest;

public static class AutoSender
{
    public static void StartSending()
    {
        string botToken = BotData.Token;
        var botClient = new TelegramBotClient(botToken);
        var sendHelper = new SendHelper();

        var firstScheduledTask = new ScheduledTask(async () => await sendHelper.SendJoke(botClient), 1);
        var secondScheduledTask = new ScheduledTask(async () => await sendHelper.SendFact(botClient), 1.5);
        firstScheduledTask.Start();
        secondScheduledTask.Start();
    }
}