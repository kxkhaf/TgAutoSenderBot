using TelegramTest;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello Just Smile");
AutoSender.StartSending();

app.Run();
