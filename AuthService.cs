namespace CodeReviewCsharpTest;

public class AuthService
{
    // Vulnerability: Hardcoded JWT secret key
    private const string JwtSecret = "super_secret_dotnet_jwt_signing_key_12345";

    public bool Authenticate(string user, string pass)
    {
        // Vulnerability: Plaintext credential logging
        Console.WriteLine($"Login attempt for user={user}, pass={pass}");
        return user == "admin" && pass == "secret";
    }
}
