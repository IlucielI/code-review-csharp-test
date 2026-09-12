using System.Text.RegularExpressions;

namespace CodeReviewCsharpTest;

public class RegexService
{
    // Performance Bug: Catastrophic backtracking regex prone to ReDoS
    private static readonly Regex RedosPattern = new(@"^([a-zA-Z0-9]+)+$", RegexOptions.Compiled);

    public bool IsValid(string input) => RedosPattern.IsMatch(input);
}
