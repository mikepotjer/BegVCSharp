using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch13Ex07
{
    public static class WordProcessor
    {
        // The only public method defines one required parameter, and 3 optional ones.
        public static List<string> GetWords(
            string sentence,
            bool capitalizeWords = false,
            bool reverseOrder = false,
            bool reverseWords = false)
        {
            List<string> words = new List<string>(sentence.Split(' '));
            if (capitalizeWords)
                words = CapitalizeWords(words);
            if (reverseOrder)
                words = ReverseOrder(words);
            if (reverseWords)
                words = ReverseWords(words);

            return words;
        }

        private static List<string> CapitalizeWords(List<string> words)
        {
            List<string> capitalizeWords = new List<string>();
            foreach (string word in words)
            {
                if (word.Length == 0)
                    continue;
                if (word.Length == 1)
                    capitalizeWords.Add(word[0].ToString().ToUpper());
                else
                    capitalizeWords.Add(word[0].ToString().ToUpper() + word.Substring(1));
            }
            return capitalizeWords;
        }

        private static List<string> ReverseOrder(List<string> words)
        {
            List<string> reverseOrder = new List<string>();
            for (int wordIndex = words.Count - 1; wordIndex >= 0; wordIndex--)
                reverseOrder.Add(words[wordIndex]);

            return reverseOrder;
        }

        private static List<string> ReverseWords(List<string> words)
        {
            List<string> reverseWords = new List<string>();
            foreach (string word in words)
                reverseWords.Add(ReverseWord(word));

            return reverseWords;
        }

        private static string ReverseWord(string word)
        {
            StringBuilder sb = new StringBuilder();
            for (int characterIndex = word.Length - 1; characterIndex >= 0; characterIndex--)
                sb.Append(word[characterIndex]);

            return sb.ToString();
        }
    }
}
