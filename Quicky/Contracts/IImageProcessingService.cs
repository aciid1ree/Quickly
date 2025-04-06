namespace Quickly.Contracts;

public interface IImageProcessingService
{
    MemoryStream ProcessingImage(MemoryStream imageStream);
}