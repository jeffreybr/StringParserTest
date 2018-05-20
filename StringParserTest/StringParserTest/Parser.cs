using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StringParserTest.Extensions;

namespace StringParserTest
{
    public class Parser
    {
        /// <summary>
        /// Gets the frequently used words in the string.
        /// </summary>
        /// <param name="source">The string sourece</param>
        /// <returns>returns the top 10 used words and their appearance count</returns>
        public Task<IList<KeyValuePair<string, int>>> GetFrequentlyUsedWordsAsync(string source)
        {
            string[] words = source.GetWordsFromString();

            var result = words
                .GroupBy(s => s)
                .Where(x => x.Key != String.Empty)
                .OrderByDescending(g => g.Count())
                .Select(c => new KeyValuePair<string, int>(c.Key, c.Count()))
                .Take(10)
                .ToList();            

            return Task.FromResult<IList<KeyValuePair<string, int>>>(result);
        }

        /// <summary>
        /// Gets all the characters and word count in the string.
        /// </summary>
        /// <param name="source">The string source</param>
        /// <returns>returns a tuple of Item1 = total character count. Item2 = total word count</returns>
        public Task<Tuple<int, int>> GetCharactersAndWordsAsync(string source)
        {
            string[] words = source.GetWordsFromString();

            var result = words
                .GroupBy(s => s)                
                .Select(g => g.Key)
                .ToList();            

            return Task.FromResult(new Tuple<int, int>(source.Length, result.Count));           
        }

        /// <summary>
        /// Gets all the characters and how many times they were used in the string.
        /// This includes special char '\O', '.' and ' '.
        /// </summary>
        /// <param name="source">The string source</param>
        /// <returns>returns the character and output count</returns>
        public Task<IList<KeyValuePair<char, int>>> GetAllCharactersAndCountAsync(string source)
        {
            char[] words = source.ToCharArray();

            var result = words
                .GroupBy(s => s)
                .Select(c => new KeyValuePair<char, int>(c.Key, c.Count()))
                .OrderByDescending(g => g.Value)
                .ToList();            

            return Task.FromResult<IList<KeyValuePair<char, int>>>(result);
        }

        /// <summary>
        /// Gets the top 5 largest words used. Largest in terms of char count.
        /// </summary>
        /// <param name="source">The string source</param>
        /// <returns>returns the word and the length of the string</returns>
        public Task<IList<KeyValuePair<string, int>>> GetLargestWordsAsync(string source)
        {
            string[] words = source.GetWordsFromString();

            var result = words
                .GroupBy(s => s)                
                .Select(c => new KeyValuePair<string, int>(c.Key, c.Key.Length))
                .OrderByDescending(g => g.Value)
                .Take(5)
                .ToList();
            

            return Task.FromResult<IList<KeyValuePair<string, int>>>(result);            
        }

        /// <summary>
        /// Gets the top 5 smallest words used. Smallest in terms of char count.
        /// </summary>
        /// <param name="source">The string source</param>
        /// <returns>returns the word and the length of the string</returns>
        public Task<IList<KeyValuePair<string, int>>> GetSmallestWordsAsync(string source)
        {
            string[] words = source.GetWordsFromString();

            var result = words
                .GroupBy(s => s)
                .Where(x => x.Key != String.Empty)
                .Select(c => new KeyValuePair<string, int>(c.Key, c.Key.Length))
                .OrderBy(g => g.Value)
                .Take(5)
                .ToList();            

            return Task.FromResult<IList<KeyValuePair<string, int>>>(result);            
        }
    }    
}