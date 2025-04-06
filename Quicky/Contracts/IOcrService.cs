namespace Quickly.Contracts;

public interface IOcrService
{
    IEnumerable<string> GetImageText(MemoryStream imageStream);
    
}