using Microsoft.AspNetCore.Mvc;

namespace CodeReviewCsharpTest;

[ApiController]
public class RedirectEndpoint : ControllerBase
{
    [HttpGet("/redirect")]
    public IActionResult RedirectUser([FromQuery] string url)
    {
        // Vulnerability: Open redirect without host validation
        return Redirect(url);
    }
}
