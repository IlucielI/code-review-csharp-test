using System.Data.SQLite;

namespace CodeReviewCsharpTest;

public class UserRepository
{
    public string? GetUserByUsername(string username)
    {
        using var conn = new SQLiteConnection("Data Source=app.db;");
        conn.Open();
        // Vulnerability: Raw SQL injection via direct string concatenation
        var query = "SELECT id, username, role FROM users WHERE username = '" + username + "'";
        using var cmd = new SQLiteCommand(query, conn);
        return cmd.ExecuteScalar()?.ToString();
    }
}
