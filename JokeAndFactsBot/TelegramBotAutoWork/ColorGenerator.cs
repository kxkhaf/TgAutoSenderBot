namespace TelegramTest;

public class ColorGenerator
{
    private Random random;

    public ColorGenerator()
    {
        random = new Random();
    }
    
    Rgba32 GenerateLightColor()
    {
        byte r = (byte)(random.Next(150, 240)); // Значение R будет от 200 до 255
        byte g = (byte)(random.Next(150, 240)); // Значение G будет от 200 до 255
        byte b = (byte)(random.Next(150, 240)); // Значение B будет от 200 до 255

        return new Rgba32(r, g, b, 255);
    }
    
    Rgba32 GenerateMiddleColor()
    {
        byte r = (byte)(random.Next(115, 240)); // Значение R будет от 150 до 255
        byte g = (byte)(random.Next(115, 240)); // Значение G будет от 150 до 255
        byte b = (byte)(random.Next(115, 240)); // Значение B будет от 150 до 255

        return new Rgba32(r, g, b, 255);
    }

    Rgba32 GenerateVibrantColor()
    {
        byte r = (byte)(random.Next(75, 240)); // Значение R будет от 150 до 255
        byte g = (byte)(random.Next(75, 240)); // Значение G будет от 150 до 255
        byte b = (byte)(random.Next(75, 240)); // Значение B будет от 150 до 255

        return new Rgba32(r, g, b, 255);
    }
    
    Rgba32 GenerateRandomColor()
    {
        byte r = (byte)random.Next(0, 256); // Значение R будет от 0 до 255
        byte g = (byte)random.Next(0, 256); // Значение G будет от 0 до 255
        byte b = (byte)random.Next(0, 256); // Значение B будет от 0 до 255

        return new Rgba32(r, g, b, 255);
    }
    public Rgba32[][] GenerateColorPairs(int count)
    {
        var colorPairs = new Rgba32[count][];

        for (int i = 0; i < count; i++)
        {
            colorPairs[i] = new Rgba32[] { GenerateLightColor(), GenerateVibrantColor() };
        }

        return colorPairs;
    }
    
    public Rgba32[][] GenerateRandomColorPairs(int count)
    {
        var colorPairs = new Rgba32[count][];

        for (int i = 0; i < count; i++)
        {
            colorPairs[i] = new Rgba32[] { GenerateRandomColor(), GenerateRandomColor() };
        }

        return colorPairs;
    }
    
    public Rgba32[] GenerateColorPair()
    {
        return new Rgba32[] { GenerateLightColor(), GenerateVibrantColor() };
    }
    
    public Rgba32[] GenerateRandomColorPair(int count)
    {
        return new Rgba32[] { GenerateRandomColor(), GenerateRandomColor() };
    }
    
    
    public Rgba32[][] GenerateThreeColor(int count)
    {
        var colorPairs = new Rgba32[count][];

        for (int i = 0; i < count; i++)
        {
            colorPairs[i] = new Rgba32[] { GenerateLightColor(), GenerateMiddleColor(), GenerateVibrantColor() };
        }

        return colorPairs;
    }
    
    public Rgba32[][] GenerateRandomThreeColor(int count)
    {
        var colorPairs = new Rgba32[count][];

        for (int i = 0; i < count; i++)
        {
            colorPairs[i] = new Rgba32[] { GenerateRandomColor(), GenerateRandomColor(), GenerateRandomColor() };
        }

        return colorPairs;
    }
    
    public Rgba32[] GenerateThreeColor()
    {
        return new Rgba32[] { GenerateLightColor(), GenerateVibrantColor() };
    }
    
    public Rgba32[] GenerateRandomThreeColor()
    {
        return new Rgba32[] { GenerateRandomColor(), GenerateRandomColor(), GenerateRandomColor() };
    }
}