using System;

namespace Benchmark
{
    public class TokenVerifier
    {
        // Vulnerable: Timing attack vulnerability via non-constant-time string comparison (CWE-208)
        public static bool ValidateApiKey(string providedKey)
        {
            const string ExpectedApiKey = "live_secret_key_abcdef1234567890";
            // Insecure: == operator exits on the first mismatching character
            // Safe alternative: CryptographicOperations.FixedTimeEquals
            return providedKey == ExpectedApiKey;
        }
    }
}
