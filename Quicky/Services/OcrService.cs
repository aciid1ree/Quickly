using Tesseract;

namespace Quickly.Services;

public class OcrService
{
    private static TesseractEngine _engine;

    public OcrService()
    {
        _engine = new TesseractEngine(@"./tessdata", "rus+eng", EngineMode.Default);
    }
    
    public IEnumerable<string> GetImageText(MemoryStream imageStream)
    {
        using var pix = Pix.LoadFromMemory(imageStream.ToArray());
        using var page = _engine.Process(pix);
        
        var text = page.GetText();
        return text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
    }   
    
    public static void Dispose()
    {
        _engine.Dispose();
    }
}