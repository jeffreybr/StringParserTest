using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringParserTest.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public async Task GetFrequentlyUsedWords_FirstIndex_ReturnsMostUsed()
        {
            Parser parser = new Parser();

            string source = "the quick brown fox jump over the lazy dog. the slow dog jump below the not lazy fox. peter pipper";
            var list = await parser.GetFrequentlyUsedWordsAsync(source);

            Assert.IsInstanceOfType(list, typeof(IList<KeyValuePair<string, int>>));

            Assert.AreEqual("the", list.First().Key);
            Assert.AreEqual(4, list.First().Value);            
        }

        [TestMethod]
        public async Task GetFrequentlyUsedWords_LastIndex_ReturnsLeastUsed()
        {
            Parser parser = new Parser();

            string source = "peter the quick brown fox jump over the slow and lazy dog. the quick brown fox jump over the lazy dog. the slow dog jump over lazy fox.";
            var list = await parser.GetFrequentlyUsedWordsAsync(source);

            Assert.AreEqual("peter", list.Last().Key);
            Assert.AreEqual(1, list.Last().Value);
        }

        [TestMethod]
        public async Task GetFrequentlyUsedWords_ReturnsMax10Rows()
        {
            Parser parser = new Parser();

            string source = "one two three four five six.seven eight nine 10 eleven";
            var list = await parser.GetFrequentlyUsedWordsAsync(source);

            Assert.AreEqual(10, list.Count);            
        }

        [TestMethod]
        public async Task GetCharactersAndWords_ReturnsTotalCharCount_AndTotalWordCount()
        {
            Parser parser = new Parser();

            string source = "one two three.four five six";
            var list = await parser.GetCharactersAndWordsAsync(source);

            Assert.IsInstanceOfType(list, typeof(Tuple<int, int>));

            //Item1 Char Count
            Assert.AreEqual(27, list.Item1);

            //Item2 Word Count
            Assert.AreEqual(6, list.Item2);
        }

        [TestMethod]
        public async Task GetAllCharactersAndCount_ReturnsAllCharCountAndCharOutputCount_AndInDescendingOrder()
        {
            Parser parser = new Parser();

            string source = "aabeee";
            var list = await parser.GetAllCharactersAndCountAsync(source);

            Assert.AreEqual(3, list.Count);

            Assert.IsInstanceOfType(list, typeof(IList<KeyValuePair<char, int>>));

            Assert.AreEqual('e', list[0].Key);
            Assert.AreEqual(3, list[0].Value);

            Assert.AreEqual('a', list[1].Key);
            Assert.AreEqual(2, list[1].Value);

            Assert.AreEqual('b', list[2].Key);
            Assert.AreEqual(1, list[2].Value);
        }

        [TestMethod]
        public async Task GetAllCharactersAndCount_ReturnsInDescendingOrderByCharCount()
        {
            Parser parser = new Parser();

            string source = "aaeee";
            var list = await parser.GetAllCharactersAndCountAsync(source);            

            Assert.AreEqual('e', list.First().Key);
            Assert.AreEqual(3, list.First().Value);

            Assert.AreEqual('a', list.Last().Key);
            Assert.AreEqual(2, list.Last().Value);            
        }

        [TestMethod]
        public async Task GetLargestWords_ReturnsTheLargestWordInCharCount_AndInDescendingOrder()
        {
            Parser parser = new Parser();

            string source = "one seven twentyone eleven";
            var list = await parser.GetLargestWordsAsync(source);

            Assert.IsInstanceOfType(list, typeof(IList<KeyValuePair<string, int>>));

            Assert.AreEqual("twentyone", list.First().Key);
            Assert.AreEqual(9, list.First().Value);

            Assert.AreEqual("one", list.Last().Key);
            Assert.AreEqual(3, list.Last().Value);
        }

        [TestMethod]
        public async Task GetLargestWords_ReturnsMax5Rows()
        {
            Parser parser = new Parser();

            string source = "one three four seven twentyone eleven";
            var list = await parser.GetLargestWordsAsync(source);

            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public async Task GetSmallesttWords_ReturnsTheSmallestWordInCharCount_AndInAscendingOrder()
        {
            Parser parser = new Parser();

            string source = "seven twentyone us eleven one two four eighty";
            var list = await parser.GetSmallestWordsAsync(source);

            Assert.IsInstanceOfType(list, typeof(IList<KeyValuePair<string, int>>));

            Assert.AreEqual("us", list.First().Key);
            Assert.AreEqual(2, list.First().Value);

            Assert.AreEqual("seven", list.Last().Key);
            Assert.AreEqual(5, list.Last().Value);
        }

        [TestMethod]
        public async Task GetSmallestWords_ReturnsMax5Rows()
        {
            Parser parser = new Parser();

            string source = "seven twentyone us eleven one two four eighty";
            var list = await parser.GetSmallestWordsAsync(source);

            Assert.AreEqual(5, list.Count);
        }

    }
}
