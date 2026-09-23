using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace CodeReviewCsharpTest.Services
{
    public class SecurityService
    {
        // Bug 1: SQL Injection via raw SQL string concatenation
        public string GetUserName(string userId, SqlConnection conn)
        {
            var cmd = new SqlCommand("SELECT Name FROM Users WHERE Id = '" + userId + "'", conn);
            return (string)cmd.ExecuteScalar();
        }

        // Bug 2: Process Command Injection
        public void RunDiagnostic(string host)
        {
            Process.Start("cmd.exe", "/c ping " + host);
        }

        // Bug 3: IDisposable Resource Leak (StreamReader not in using statement)
        public string ReadConfig(string path)
        {
            var reader = new StreamReader(path);
            return reader.ReadToEnd(); // Not closed/disposed
        }

        // Safe Guard: Parameterized query with SqlParameter (MUST NOT be flagged as SQLi)
        public string GetUserNameSafe(string userId, SqlConnection conn)
        {
            using var cmd = new SqlCommand("SELECT Name FROM Users WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", userId);
            return (string)cmd.ExecuteScalar();
        }
    }
}
