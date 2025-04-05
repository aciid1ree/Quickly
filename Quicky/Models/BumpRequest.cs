namespace Quicky.Models;

public class BumpRequest
{
    public string UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; }
}