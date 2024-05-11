namespace TelegramTest;

public static class EnumExtensions
{
    public static string GetAttributeDescription(this Enum enumValue)
    {
        if (enumValue == null)
            throw new ArgumentNullException(nameof(enumValue));

        Type enumType = enumValue.GetType();
        string? name = Enum.GetName(enumType, enumValue);
        if (name is null) return string.Empty;
        var field = enumType.GetField(name);

        var attribute = field?.GetCustomAttributes(false)
            .OfType<System.ComponentModel.DescriptionAttribute>()
            .FirstOrDefault();

        return attribute?.Description ?? string.Empty;
    }
}