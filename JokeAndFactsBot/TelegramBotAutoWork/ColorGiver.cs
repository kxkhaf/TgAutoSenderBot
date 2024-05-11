namespace TelegramTest;

public class ColorGiver
{

    private Random random { get; set; } = new();
    
    public Rgba32[] GetColor(ColorType colorType)
    {
        if (colorType == ColorType.TwoColor)
        {
            return random.Next(0, 2) switch
            {
                1 => ColorCollector.TwoColorsArray[random.Next(ColorCollector.TwoColorsArray.Length)],
                _ => new ColorGenerator().GenerateColorPair()
            };
        }

        return random.Next(0, 2) switch
        {
            1 => ColorCollector.ThreeColorsArray[random.Next(ColorCollector.ThreeColorsArray.Length)],
            _ => new ColorGenerator().GenerateThreeColor()
        };
    }
}