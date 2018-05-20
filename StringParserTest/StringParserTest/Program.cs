using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevTest;
using StringParserTest.Extensions;
using Timer = System.Timers.Timer;

namespace StringParserTest
{
    internal class Program
    {
        private static Timer _timer;
        private static Parser _parser;

        private static CancellationTokenSource _tokenSource;
        private static CancellationToken _token;

        //The final output string concatenating the random string generated every 2 seconds. Does not include special char '\O' which is equivalent to space
        private static string _outputString;  
        //The concatenated raw string including the special car to be included on the char count.
        private static string _rawTextString;

        public static void Main(string[] args)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            //Create a timer with 2 second interval to continuously output a text.
            _timer = new Timer
            {
                Interval = 2000,
                AutoReset = true
            };
            _timer.Elapsed += delegate
            {
                using (LorumIpsumStream streamWriter = new LorumIpsumStream())
                {
                    //Generate a random stream of 250 bytes of random chars
                    byte[] streamBytes = new byte[250];

                    streamWriter.ReadAsync(streamBytes, 0, streamBytes.Length);
                    streamWriter.BeginWrite(streamBytes, 0, 0, OnStreamWrite, streamBytes);
                }               
            };
            _timer.Start();

            _parser = new Parser();

            Console.WriteLine("Press \'s\' to stop.\r\nPress any key to exit.");
            while (Console.ReadKey().KeyChar == 's')
            {
                _timer.Stop();
                _tokenSource.Cancel();
            }
        }

        private static void OnStreamWrite(IAsyncResult ar)
        {
            try
            {                
                Task.Run(() =>
                {
                    if (_token.IsCancellationRequested)
                    {
                        Console.WriteLine("Task was cancelled.");
                        _token.ThrowIfCancellationRequested();
                    }

                    byte[] strBytes = (byte[]) ar.AsyncState;

                    System.Text.Encoding encoding = System.Text.Encoding.ASCII;
                    string rawText = encoding.GetString(strBytes);                    

                    Console.WriteLine($"New Random String: {rawText}\r\n");

                    _outputString += rawText.CleanText();
                    _rawTextString += rawText;

                    Console.WriteLine($"Final Output String: {_outputString}\r\n");
                    
                    var getCharsAndWordsTask = _parser.GetCharactersAndWordsAsync(_rawTextString);
                    var getCharactersUsedAndCountTask = _parser.GetAllCharactersAndCountAsync(_rawTextString);
                    var getFrequentWordsTask = _parser.GetFrequentlyUsedWordsAsync(_outputString);
                    var getLargestWordsTask = _parser.GetLargestWordsAsync(_outputString);
                    var getSmallestWordsTask = _parser.GetSmallestWordsAsync(_outputString);                    

                    Task.WaitAll(
                        getCharsAndWordsTask,
                        getFrequentWordsTask,
                        getLargestWordsTask,
                        getSmallestWordsTask,
                        getCharactersUsedAndCountTask
                    );

                    //Total number of chars and words output.
                    Console.WriteLine($"Total number of Characters: {getCharsAndWordsTask.Result.Item1}. Total number of words: {getCharsAndWordsTask.Result.Item2}.\r\n");

                    AllCharactersAndCountOutput(getCharactersUsedAndCountTask.Result);
                    FrequentWordsOutput(getFrequentWordsTask.Result);
                    LargestWordsOutput(getLargestWordsTask.Result);
                    SmallestWordsOutput(getSmallestWordsTask.Result);

                }, _token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);                
                _timer.Dispose();
                _tokenSource.Dispose();
            }            
        }

        /// <summary>
        /// Displays all the character used and their output count.
        /// </summary>        
        private static void AllCharactersAndCountOutput(IList<KeyValuePair<char, int>> collection)
        {
            var output = string.Empty;
            foreach (var item in collection)
            {
                if (item.Key == '\0')
                {
                    output += $"\tchar is \'\\0\', count={item.Value}: {item}\r\n";
                }                
                else
                {
                    output += $"\tchar is '{item.Key}', count={item.Value}: {item}\r\n";
                }                
            }
            Console.WriteLine($"Show all used characters including their count in descending order.\r\n{output}");
        }

        /// <summary>
        /// Displays all the Smallest words from the collection.
        /// Also shows the words character count.
        /// </summary>
        private static void SmallestWordsOutput(IList<KeyValuePair<string, int>> collection)
        {
            var output = string.Empty;
            foreach (var item in collection)
            {
                output += $"\tword '{item.Key}', count={item.Value} : {item}\r\n";
            }
            Console.WriteLine($"Show top 5 smallest words.\r\n{output}");
        }

        /// <summary>
        /// Displays all the Largest words from the collection.
        /// Also shows the words character count
        /// </summary>
        private static void LargestWordsOutput(IList<KeyValuePair<string, int>> collection)
        {            
            var output = string.Empty;
            foreach (var item in collection)
            {
                output += $"\tword '{item.Key}', count={item.Value} : {item}\r\n";
            }
            Console.WriteLine($"Show top 5 largest words.\r\n{output}");
        }

        /// <summary>
        /// Displays all the Frequent words from the collection.
        /// Also shows the words frequent count.
        /// </summary>
        private static void FrequentWordsOutput(IList<KeyValuePair<string, int>> collection)
        {            
            var output = string.Empty;
            foreach (KeyValuePair<string, int> str in collection)
            {
                output += $"\t word '{str.Key}', count={str.Value}\r\n";
            }
            Console.WriteLine($"Show 10 most frequently used words.\r\n{output}");
        }
    }
}