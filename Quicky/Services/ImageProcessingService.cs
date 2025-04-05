using System.Drawing;
using System.Drawing.Imaging;

namespace Quickly.Services;

public class ImageProcessingService
{
    public MemoryStream ProcessingImage(MemoryStream imageStream)
    {
        var image = new Bitmap(imageStream);

        var grayscaleImage = ConvertToGrayscale(image);

        var contrastImage = IncreaseContrast(grayscaleImage);

        var binaryImage = BinarizeImage(contrastImage);

        var processedStream = new MemoryStream();
        binaryImage.Save(processedStream, ImageFormat.Jpeg);
        processedStream.Position = 0; 

        return processedStream;
    }
    
    private Bitmap ConvertToGrayscale(Bitmap image)
    {
        var width = image.Width;
        var height = image.Height;
        var grayscaleImage = new Bitmap(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pixel = image.GetPixel(x, y);
                var grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                var grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                grayscaleImage.SetPixel(x, y, grayColor);
            }
        }
        return grayscaleImage;
    }

    private Bitmap IncreaseContrast(Bitmap image)
    {
        var contrast = 2.0f;  
        var contrastImage = new Bitmap(image);
        var matrix = new float[][]
        {
            [contrast, 0, 0, 0, 0],
            [0, contrast, 0, 0, 0],
            [0, 0, contrast, 0, 0],
            [0, 0, 0, 1, 0],
            [0, 0, 0, 0, 1]
        };

        var imageAttributes = new ImageAttributes();
        imageAttributes.SetColorMatrix(new ColorMatrix(matrix));
        var graphics = Graphics.FromImage(contrastImage);
        graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
            0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);

        return contrastImage;
    }

    private Bitmap BinarizeImage(Bitmap image)
    {
        var width = image.Width;
        var height = image.Height;
        var threshold = 128; 
        var binaryImage = new Bitmap(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel = image.GetPixel(x, y);
                var grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                var color = grayValue > threshold ? Color.White : Color.Black;
                binaryImage.SetPixel(x, y, color);
            }
        }

        return binaryImage;
    }
}