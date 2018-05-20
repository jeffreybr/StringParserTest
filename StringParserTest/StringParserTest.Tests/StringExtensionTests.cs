using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringParserTest.Extensions;

namespace StringParserTest.Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void GetsWordsFromString_SeparatedBySpace()
        {
            string str = "The quick brown fox jumps over the lazy dog";
            string[] words = str.GetWordsFromString();

            Assert.AreEqual(words[0], "The");
            Assert.AreEqual(words[1], "quick");
            Assert.AreEqual(words[2], "brown");
            Assert.AreEqual(words[3], "fox");
            Assert.AreEqual(words[4], "jumps");
            Assert.AreEqual(words[5], "over");
            Assert.AreEqual(words[6], "the");
            Assert.AreEqual(words[7], "lazy");
            Assert.AreEqual(words[8], "dog");
        }

        [TestMethod]
        public void GetsWordsFrom_StringSeparatedByPeriod()
        {
            string str = "The.quick.brown.fox.jumps.over.the.lazy.dog";
            string[] words = str.GetWordsFromString();

            Assert.AreEqual(words[0], "The");
            Assert.AreEqual(words[1], "quick");
            Assert.AreEqual(words[2], "brown");
            Assert.AreEqual(words[3], "fox");
            Assert.AreEqual(words[4], "jumps");
            Assert.AreEqual(words[5], "over");
            Assert.AreEqual(words[6], "the");
            Assert.AreEqual(words[7], "lazy");
            Assert.AreEqual(words[8], "dog");
        }

        [TestMethod]
        public void GetsWordsFromStringSeparatedBy_PeriodAndSpace()
        {
            string str = "The quick brown.fox jumps over.the lazy.dog";
            string[] words = str.GetWordsFromString();

            Assert.AreEqual(words[0], "The");
            Assert.AreEqual(words[1], "quick");
            Assert.AreEqual(words[2], "brown");
            Assert.AreEqual(words[3], "fox");
            Assert.AreEqual(words[4], "jumps");
            Assert.AreEqual(words[5], "over");
            Assert.AreEqual(words[6], "the");
            Assert.AreEqual(words[7], "lazy");
            Assert.AreEqual(words[8], "dog");
        }

        [TestMethod]
        public void CleanTextFromString_RemovesTrailingSpaces()
        {
            string raw = "The quick brown.\0fox jumps over.\0the lazy.\0dog";
            string output = raw.CleanText();

            Assert.AreEqual("The quick brown.fox jumps over.the lazy.dog", output);
        }

        [TestMethod]
        public void RemovesTrailingSpaces_GetsWords()
        {
            string raw = "The quick brown.\0fox jumps over.\0the lazy.\0dog";
            string output = raw.CleanText();

            Assert.AreEqual("The quick brown.fox jumps over.the lazy.dog", output);

            string[] words = output.GetWordsFromString();

            Assert.AreEqual(words[0], "The");
            Assert.AreEqual(words[1], "quick");
            Assert.AreEqual(words[2], "brown");
            Assert.AreEqual(words[3], "fox");
            Assert.AreEqual(words[4], "jumps");
            Assert.AreEqual(words[5], "over");
            Assert.AreEqual(words[6], "the");
            Assert.AreEqual(words[7], "lazy");
            Assert.AreEqual(words[8], "dog");
        }
    }
}
