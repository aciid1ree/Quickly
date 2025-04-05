using Microsoft.AspNetCore.Mvc;
using Quicky.Models;

namespace Quickly.Controllers;

[ApiController]
public class BumpController : ControllerBase
{
    [HttpPost]
    public IActionResult PostBump([FromBody] BumpRequest bumpRequest)
    {
        if (bumpRequest == null)
            return BadRequest();

        bumpRequest.Timestamp = DateTime.UtcNow;
        MemoryStore.Bumps.Add(bumpRequest);

        return Ok(new { message = "Bump was added", userId = bumpRequest.UserId });
    }

    [HttpGet]
    public IActionResult GetBumpSession()
    {
        var session = (MemoryStore.Bumps
            .GroupBy(time => time.Timestamp)
            .OrderByDescending(g => g.Key)
            .FirstOrDefault() ?? throw new InvalidOperationException()).ToList();
        
        var userIds = session.Select(time => time.UserId);
        return Ok(new { participians = userIds });
    }
}