# 🤖 AutoJokes Bot - Telegram Bot for Scheduled Jokes & Facts

[![Telegram](https://img.shields.io/badge/Telegram-2CA5E0?logo=telegram)](https://t.me/YourBotName)
[![.NET](https://img.shields.io/badge/.NET-6.0-purple?logo=dotnet)](https://dotnet.microsoft.com)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

Automated Telegram bot that periodically sends jokes and interesting facts to group chats using external APIs.

## 🌟 Features

- **Automatic scheduled messages** with configurable intervals
- **Dual content system**:
  - 🎭 Random jokes from joke API
  - 📚 Interesting facts from fact API
- **Flexible scheduling** (adjustable timing for each content type)
- **Easy integration** with any Telegram group
- **Error handling** and automatic retries

## 🛠 Technical Implementation

```csharp
// Example scheduling configuration
var firstScheduledTask = new ScheduledTask(
    async () => await sendHelper.SendJoke(botClient), 
    1); // Sends jokes every 1 hour

var secondScheduledTask = new ScheduledTask(
    async () => await sendHelper.SendFact(botClient),
    1.5); // Sends facts every 1.5 hours```