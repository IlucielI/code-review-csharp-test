using Microsoft.AspNetCore.Mvc;

namespace CodeReviewCsharpTest;

[ApiController]
public class LoginEndpoint : ControllerBase
{
    // Missing rate limiting on sensitive authentication route
    [HttpPost("api/login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        return Ok(new { token = "auth-token-123" });
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
