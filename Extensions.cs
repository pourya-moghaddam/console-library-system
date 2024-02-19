using System;
using System.Text.RegularExpressions;

namespace LibrarySystem
{
    public static class Extensions
    {
        public static string Capitalize(this string str)
        {
            // Capitalize all words in string 'str'
            string result = Regex.Replace(
                str.ToLower(),
                @"\b(\w)",
                m => m.Value.ToUpper()
            );
            result = Regex.Replace(
                result, @"(\s(of|in|by|and)|\'[st])\b",
                m => m.Value.ToLower(),
                RegexOptions.IgnoreCase
            );
            return result;
        }
    }
}
