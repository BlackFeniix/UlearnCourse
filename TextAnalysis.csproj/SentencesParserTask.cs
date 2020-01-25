using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentences = text.ToLower().Split(new char[] { '.', '!', '?', ';', ':', '(', ')'}, StringSplitOptions.RemoveEmptyEntries);
            foreach(var sentence in sentences)
            {
                List<string> wordList = new List<string>();
                var words = sentence.Split(new char[] { ' '});
                foreach (var word in words)
                {
                    var symbols = word.ToCharArray();
                    string newWord = "";
                    foreach (var symbol in symbols)
                    {
                        if (char.IsLetter(symbol) || symbol == '\'')
                            newWord = newWord + symbol;
                        else
                        {
                            if (newWord != "")
                                wordList.Add(newWord);
                            newWord = "";
                        }
                    }
                    if (newWord!="")
                        wordList.Add(newWord);
                }
                if (wordList.Count>0)
                    sentencesList.Add(wordList);
            }
            return sentencesList;
        }
    }
}