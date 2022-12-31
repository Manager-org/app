using System.Text.RegularExpressions;

namespace Manager.Utils
{
    public static class Verification
    {
        public static bool IsGoodText(string text) => !Regex.IsMatch(text, @"[^A-z 0-9]");
        public static bool IsGoodNumber(string text) => !Regex.IsMatch(text, @"[^0-9]");
    }
}
