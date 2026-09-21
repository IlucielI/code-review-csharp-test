using Microsoft.AspNetCore.Http;

namespace CodeReviewCsharpTest;

public class CookieHelper
{
    // Insecure cookie: setting auth token cookie with HttpOnly = false and Secure = false
    public void AppendAuthCookie(IResponseCookies cookies, string token)
    {
        cookies.Append("session_token", token, new CookieOptions
        {
            HttpOnly = false,
            Secure = false
        });
    }
}
