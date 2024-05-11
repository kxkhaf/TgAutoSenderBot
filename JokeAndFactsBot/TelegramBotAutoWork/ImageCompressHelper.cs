using ImageMagick;

namespace TelegramTest;

public class ImageCompressHelper
{
    public static async Task Compress(string imagePath)
    {
        using var image = new MagickImage(imagePath);
        image.Strip(); // Remove any metadata
        image.Quality = 7; // Adjust the image quality (0-100) - lower values mean higher compression
        await image.WriteAsync(imagePath); // Overwrite the original image with the compressed version
    }
}