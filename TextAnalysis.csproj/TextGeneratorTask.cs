using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            for (int i=0; i<wordsCount; i++)
            {
                var words = phraseBeginning.Split(' ');
                string lastTwoWords = "";
                string lastWord = words[words.Length-1];

                if (words.Length > 1)
                    lastTwoWords = words[words.Length - 2] + " " + words[words.Length-1];

                if (lastTwoWords != "" && nextWords.ContainsKey(lastTwoWords))
                    phraseBeginning = phraseBeginning + " " + nextWords[lastTwoWords];
                else if (nextWords.ContainsKey(lastWord))
                {
                    phraseBeginning = phraseBeginning + " " + nextWords[lastWord];
                }
                else
                    return phraseBeginning;
            }
            return phraseBeginning;
        }
    }
}