using Microsoft.AspNetCore.Mvc;

namespace CodeReviewCsharpTest;

[ApiController]
public class UserProfileEndpoint : ControllerBase
{
    private static readonly Dictionary<int, string> UserStore = new() { { 1, "Alice" }, { 2, "Bob" } };

    [HttpDelete("/users/{id}")]
    public IActionResult DeleteUser(int id)
    {
        // Vulnerability: IDOR missing authentication and authorization checks
        UserStore.Remove(id);
        return Ok(new { status = "deleted", id });
    }
}
