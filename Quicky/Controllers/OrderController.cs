using Microsoft.AspNetCore.Mvc;
using Quickly.Contracts;

namespace Quickly.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IImageProcessingService _imageProcessingService;
    private readonly IOcrService _ocrService;
    
    public OrderController(IImageProcessingService imageProcessingService, IOcrService ocrService)
    {
        _imageProcessingService = imageProcessingService;
        _ocrService = ocrService;   
    }
    
    [HttpPost("ocr")]
    public async Task<IActionResult> ProcessOrder(IFormFile image)
    {
        if (image.Length > 0)
            return BadRequest("Image was empty");
        
        using var stream = new MemoryStream();  
        await image.CopyToAsync(stream);    
        
        var processedImage = _imageProcessingService.ProcessingImage(stream);
        
        var ocrText = _ocrService.GetImageText(processedImage);

        if (ocrText == null)
            return BadRequest("Ошибка распознавания текста.");

        return Ok(new { Text = ocrText });
    } 
}