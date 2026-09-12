namespace CodeReviewCsharpTest;

public class HttpProxyService
{
    public async Task<string> FetchUrlAsync(string targetUrl)
    {
        // Vulnerability: SSRF via unvalidated user URL
        using var client = new HttpClient();
        return await client.GetStringAsync(targetUrl);
    }
}
