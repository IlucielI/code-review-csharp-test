using System.Data.SQLite;
using System.Xml;
using Microsoft.AspNetCore.Http;

namespace CodeReviewCsharpTest;

public class SafeGuards
{
    private static readonly HashSet<string> AllowedHosts = new() { "example.com", "api.example.com" };

    public string? SafeQuery(string username)
    {
        // Guard: Parameterized query using SQLiteParameter - NOT SQL injection
        using var conn = new SQLiteConnection("Data Source=app.db;");
        conn.Open();
        using var cmd = new SQLiteCommand("SELECT id, username FROM users WHERE username = @user", conn);
        cmd.Parameters.AddWithValue("@user", username);
        return cmd.ExecuteScalar()?.ToString();
    }

    public string SafeRedirect(string url)
    {
        // Guard: Validated URI host against whitelist - NOT open redirect
        if (Uri.TryCreate(url, UriKind.Absolute, out var uri) && AllowedHosts.Contains(uri.Host))
        {
            return url;
        }
        return "/home";
    }

    public int SafeReadFile(string path)
    {
        // Guard: using statement for deterministic disposal - NOT resource leak
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return (int)stream.Length;
    }

    public void SafeCookie(IResponseCookies cookies, string token)
    {
        // Guard: Secure cookie with HttpOnly and Secure enabled
        cookies.Append("session_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax
        });
    }

    public void SafeXml(string xml)
    {
        // Guard: XmlReaderSettings with DtdProcessing.Prohibit - NOT XXE
        var settings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Prohibit
        };
        using var reader = XmlReader.Create(new StringReader(xml), settings);
        while (reader.Read()) { }
    }
}
