using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using TelegramGptBot;

namespace TelegramTest;

public class SendHelper
{
    public SendHelper()
    {
        _imageCreator = new ImageCreator();
        _generatingText = new GeneratingText();
    }

    private readonly GeneratingText _generatingText;
    private readonly ImageCreator _imageCreator;
    public async Task<Message> SendJoke(ITelegramBotClient botClient)
    {
        try
        {
            var imagePath = "image2.png".GetFullFileName();
            _imageCreator.CreateJokeImage();
            await using var stream = new FileStream(imagePath, FileMode.Open);
            var fileName = imagePath.Split('/').Last(); // Extract the file name from the path
            var inputOnlineFile = new InputOnlineFile(stream, fileName);
            return await botClient.SendPhotoAsync(BotData.ChatId, inputOnlineFile, await _generatingText.GetJoke(), parseMode: ParseMode.Markdown);
            //File.Delete(imagePath);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return null!;
        }
    }

    public async Task<Message> SendFact(ITelegramBotClient botClient)
    {
        try
        {
            var imagePath = "image3.png".GetFullFileName();
            _imageCreator.CreateFactImage();
            await using var stream = new FileStream(imagePath, FileMode.Open);
            var fileName = imagePath.Split('/').Last(); // Extract the file name from the path
            var inputOnlineFile = new InputOnlineFile(stream, fileName);
            return await botClient.SendPhotoAsync(BotData.ChatId, inputOnlineFile, await _generatingText.GetFact(), parseMode: ParseMode.Markdown);
            //File.Delete(imagePath);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return null!;
        }
    }
}