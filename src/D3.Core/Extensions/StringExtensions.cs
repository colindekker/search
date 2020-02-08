namespace D3.Core.Extensions
{
    /// <summary>
    /// <c>string</c> extension methods
    /// </summary>
    public static class StringExtensions
    {
        public static string Capitalize(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return s;
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
