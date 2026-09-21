namespace CodeReviewCsharpTest;

public class DocumentReader
{
    public int CountFileBytes(string path)
    {
        // Performance / Resource Leak: FileStream created without using statement or Dispose()
        var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        var total = 0;
        while (stream.ReadByte() != -1)
        {
            total++;
        }
        return total;
    }
}
