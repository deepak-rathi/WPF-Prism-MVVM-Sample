using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace abc.infrastructure.Utils
{
    /// <summary>
    ///     Regex Utilites for validation
    /// </summary>
    public static class RegexUtilities
    {
        private static bool _invalid;

        /// <summary>
        ///     Check if Email address passed as parameter is valid email address or not
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string emailAddress)
        {
            _invalid = false;
            if (string.IsNullOrEmpty(emailAddress))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (_invalid)
                return false;

            // Return true if emailAddress is in valid e-mail format.
            try
            {
                return Regex.IsMatch(emailAddress,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Needed to support chinese character support
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();

            var domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                _invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        /// <summary>
        ///     Remove HTML tag from string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveHtmlTag(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return " ";

            return Regex.Replace((Regex.Replace(input, "<.*?>", String.Empty)), "&.*?;", " ");
        }
    }
}
