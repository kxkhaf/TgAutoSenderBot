using TelegramGptBot;

namespace TelegramTest;

public class ImageCreator
{
    public string? CreateGradientImage(string outputFileName, Rgba32[] colors, int width = 1500, int height = 1500,
        Point startGradientPoint = default, Point endGradientPoint = default, float angle = 45f)
    {
        using var image = new Image<Rgba32>(width, height);
        try
        {
            var angleRadians = angle * (float)Math.PI / 180f;

            startGradientPoint = startGradientPoint == default ? new Point(width / 2, height / 2) : startGradientPoint;
            endGradientPoint = endGradientPoint == default
                ? new Point(width * 4 / 3, height * 4 / 3)
                : endGradientPoint;
            var gradientWidth = Math.Abs(endGradientPoint.X - startGradientPoint.X);
            var gradientHeight = Math.Abs(endGradientPoint.Y - startGradientPoint.Y);

            for (var y = 0; y < height; y++)
            {
                if (y < 0 || y >= height) continue;
                var gradientPositionY = (float)(y - startGradientPoint.Y) / gradientHeight;

                for (int x = 0; x < width; x++)
                {
                    if (x < 0 || x >= width) continue;
                    var gradientPositionX = (float)(x - startGradientPoint.X) / gradientWidth;
                    var position = gradientPositionX * (float)Math.Cos(angleRadians) +
                                   gradientPositionY * (float)Math.Sin(angleRadians);
                    position = (position + 1f) / 2f; // Adjust position to [0, 1]

                    Rgba32 blendedColor = InterpolateColors(colors, position);
                    image[x, y] = blendedColor;
                }
            }

            image.Save(outputFileName);
        }
        catch
        {
            // ignored
        }

        return null;
    }

    private Rgba32 InterpolateColors(Rgba32[] colors, float position)
    {
        float colorStep = 1f / (colors.Length - 1);
        int colorIndex1 = (int)(position / colorStep);
        int colorIndex2 = colorIndex1 + 1;

        if (colorIndex2 >= colors.Length)
        {
            colorIndex2 = colors.Length - 1;
            colorIndex1 = colors.Length - 2;
        }

        float blendPosition = (position - colorIndex1 * colorStep) / colorStep;

        Rgba32 color1 = colors[colorIndex1];
        Rgba32 color2 = colors[colorIndex2];

        return Interpolate(color1, color2, blendPosition);
    }

    private Rgba32 Interpolate(Rgba32 start, Rgba32 end, float position)
    {
        byte r = (byte)(start.R + (byte)((end.R - start.R) * position));
        byte g = (byte)(start.G + (byte)((end.G - start.G) * position));
        byte b = (byte)(start.B + (byte)((end.B - start.B) * position));
        byte a = (byte)(start.A + (byte)((end.A - start.A) * position));
        return new Rgba32(r, g, b, a);
    }

    public async Task<bool> CreateImageWithColor(ColorType colorType)
    {
        try
        {
            var colorGiver = new ColorGiver();

            var fileName = $"image{(colorType == ColorType.TwoColor ? 2 : 3)}.png";
            var angle = 45f;
            if (colorType == ColorType.ThreeColor) angle = 90f;

            CreateGradientImage(fileName.GetFullFileName(),
                colorGiver.GetColor(colorType), startGradientPoint: new Point(500, 500),
                endGradientPoint: new Point(2000, 2000), angle: angle);
            
            return true;
        }
        catch
        {
            return false;
        }
    }


    public void CreateJokeImage()
    {
        CreateImage(ColorType.TwoColor);
    }
    
    public void CreateFactImage()
    {
        CreateImage(ColorType.ThreeColor);
    }
    
    public Task<bool> CreateImage(ColorType colorType)
    {
        try
        {
            var colorGiver = new ColorGiver();

            var fileName = $"image{(colorType == ColorType.TwoColor ? 2 : 3)}.png".GetFullFileName();
            var angle = 45f;
            if (colorType == ColorType.ThreeColor) angle = 90f;

             CreateGradientImage(fileName,
                colorGiver.GetColor(colorType), startGradientPoint: new Point(500, 500),
                endGradientPoint: new Point(2000, 2000), angle: angle);
            
            AddFrameAndSave(fileName);
            
            ConnectImageWithText(colorType);

            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
    
    static void AddFrameAndSave(string imagePath, Rgba32 frameColor = default, int frameThickness = 25)
    {
        frameColor = frameColor == default ? new Rgba32(0, 0, 0) : frameColor;
        // Загрузка изображения
        using Image<Rgba32> image = Image.Load<Rgba32>(imagePath);
        // Вычисляем новый размер изображения с учетом рамки
        int newWidth = image.Width + 2 * frameThickness;
        int newHeight = image.Height + 2 * frameThickness;

        // Создаем новое изображение с рамкой
        using (Image<Rgba32> framedImage = new Image<Rgba32>(newWidth, newHeight))
        {
            // Заполняем рамку выбранным цветом
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    if (x < frameThickness || x >= newWidth - frameThickness || y < frameThickness || y >= newHeight - frameThickness)
                    {
                        framedImage[x, y] = frameColor;
                    }
                    else
                    {
                        framedImage[x, y] = image[x - frameThickness, y - frameThickness];
                    }
                }
            }
            framedImage.Save(imagePath);
        }
    }

    static void ConnectImageWithText(ColorType colorType)
    {
        Random random = new Random();
        var colorCount = colorType == ColorType.TwoColor ? 2 : 3;
        var backgroundImagePath = $"image{colorCount}.png".GetFullFileName();
        var overlayImagePath = colorCount switch
        {
            2 => $"Joke{random.Next(1, 11)}.png".GetFullJFName(),
            _ => $"Fact{random.Next(1, 11)}.png".GetFullJFName(),
        };
        using var backgroundImage = Image.Load<Rgba32>(backgroundImagePath);
        using var overlayImage = Image.Load<Rgba32>(overlayImagePath);
        backgroundImage.Mutate(ctx => ctx.DrawImage(overlayImage, new Point(0, 0), 1f));
        backgroundImage.Save(backgroundImagePath);
    }
}