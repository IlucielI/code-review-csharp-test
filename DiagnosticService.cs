using System.Diagnostics;

namespace CodeReviewCsharpTest;

public class DiagnosticService
{
    public string RunPing(string host)
    {
        // Vulnerability: Command injection via unescaped shell argument
        var psi = new ProcessStartInfo
        {
            FileName = "sh",
            Arguments = "-c \"ping -c 1 " + host + "\"",
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        using var process = Process.Start(psi);
        return process?.StandardOutput.ReadToEnd() ?? string.Empty;
    }
}
