using System.Text.RegularExpressions;

namespace Pcbc.DiaperPantry.SheetIngestion.Utility
{
    public interface IRegexService
    {
        string SanitizeMySqlString(string command);
    }

    public class RegexService : IRegexService
    {
        public string SanitizeMySqlString(string command)
        {
            if (command == null)
                return string.Empty;

            // SQL Encoding for MySQL Recommended here:
            // http://au.php.net/manual/en/function.mysql-real-escape-string.php
            // it escapes \r, \n, \x00, \x1a, baskslash, single quotes, and double quotes
            return Regex.Replace(command, @"[\r\n\x00\x1a\\'""]", @"\$0");
        }
    }
}
