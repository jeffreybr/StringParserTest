namespace StringParserTest.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Remove the '\O' special character.
        /// </summary>
        /// <returns>return clean string</returns>
        public static string CleanText(this string str)
        {
            return str.Replace("\0", "");
        }

        /// <summary>
        /// Parses the words from the string source.
        /// Words from the stream generator are terminated by space and period.
        /// </summary>        
        /// <returns>returns string array of words</returns>
        public static string[] GetWordsFromString(this string str)
        {
            return str.Split(' ', '.');
        }
    }
}
