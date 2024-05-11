using HtmlAgilityPack;

namespace TelegramGptBot;

public class GeneratingText
{
    HttpClient client { get; set; }
    public GeneratingText()
    {
        client = new HttpClient();
    }

    public async Task<string> GetJoke()
    {
        return await GetText(TextType.Joke);
    }
    
    public async Task<string> GetFact()
    {
        return await GetText(TextType.Fact);
    }

    public async Task<string> GetText(TextType textType)
    {
        var url = textType switch
        {
            TextType.Joke => "https://randstuff.ru/joke/api/random/",
            TextType.Fact => "https://randstuff.ru/fact/",
            _ => null
        };

        if (url is null) return null!;

        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            var time = DateTime.Now;
            Console.WriteLine($"Failed to get the content. Time: {time}");
            return null!;
        }

        var content = await response.Content.ReadAsStringAsync();

        var doc = new HtmlDocument();
        doc.LoadHtml(content);
        var node = doc.DocumentNode.SelectSingleNode("//table[@class='text']//td");

        if (node != null)
        {
            return node.InnerText.Trim();
        }
        
        Console.WriteLine($"Text not found or HTML structure changed. Time: {DateTime.Now}");
        await SaveTextToFile(
            Path.Combine(Directory.GetParent(
                Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString() ?? string.Empty, 
                "Logging", "TextInformation.txt"), TextType.Fact);
        return null!;
    }

    private static async Task SaveTextToFile(string filePath, TextType textType)
    {
        var message = $"time: {DateTime.Now}; Text not found or HTML structure changed; {textType}";
            await File.WriteAllTextAsync(filePath, message);
    }
}