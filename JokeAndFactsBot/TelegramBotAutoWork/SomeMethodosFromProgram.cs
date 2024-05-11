using System.Text;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramTest;

public class SomeMethodosFromProgram
{
    private static Gpt3Client gpt3Client;
    private static TelegramBotClient botClient;
    private static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Message.Chat.Id);
            if (e.Message.Type == MessageType.Text)
            {
                var userMessage = e.Message.Text;
                Console.WriteLine(userMessage);
                
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "response"
                );
            }
        }
        
        
        private static async void BotOnMessageWithGPT(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
            {
                var userMessage = e.Message.Text;

                // Отправляем запрос в GPT-3
                var response = await gpt3Client.GetGpt3Response(userMessage);

                // Отправляем ответ обратно в чат
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: response
                );
            }
        }
    }

    public class Gpt3Client
    {
        private readonly string apiKey;
        private readonly HttpClient httpClient;

        public Gpt3Client(string apiKey)
        {
            this.apiKey = apiKey;
            this.httpClient = new HttpClient();
        }

        public async Task<string> GetGpt3Response(string prompt)
        {
            string apiUrl = "https://api.openai.com/v1/engines/text-davinci-003/completions";

            var requestData = new
            {
                prompt = prompt,
                temperature = 1,
                max_tokens = 1000
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var response = await httpClient.PostAsync(apiUrl, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            
            //Console.WriteLine(jsonResponse); // Выводим ответ от API для отладки

            // Проверяем, что API вернуло корректный ответ
            if (responseObject != null && responseObject.choices != null && responseObject.choices.Count > 0)
            {
                return responseObject.choices[0].text;
            }
            else
            {
                return "Failed to get a response from GPT-3 API.";
            }
        }
}