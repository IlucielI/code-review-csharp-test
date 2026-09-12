namespace CodeReviewCsharpTest;

public class FileStorageService
{
    private const string BaseDir = "/var/app/files";

    public string ReadUserFile(string filename)
    {
        // Vulnerability: Path traversal via unvalidated Path.Combine
        var fullPath = Path.Combine(BaseDir, filename);
        return File.ReadAllText(fullPath);
    }
}
