namespace TelegramGptBot;

public static class DirectoryHelper
{
    public static string GetProjectRoot()
    {
        return Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString() ??
                            string.Empty;
    }

    public static string GetFilePathFromDirectory(this string fileName, string directory = "")
    {
        return Path.Combine(GetProjectRoot(), directory, fileName);
    }
    
    public static string GetFullFileName(this string fileName)
    {
        return Path.Combine(GetFilePathFromDirectory(fileName, "ResultImages"));
    }
    public static string GetFullJFName(this string fileName)
    {
        return Path.Combine(GetFilePathFromDirectory(fileName, "FactAndJokeText"));
    }
}