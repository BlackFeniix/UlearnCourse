using System;
using System.Collections.Generic;
using System.Linq;

namespace Password2
{
    
    public class CaseAlternatorTask
    {
        
        //Вызывать будут этот метод
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
           /*
            var distinctItems = result.Distinct();
            foreach (var word in distinctItems)
            {
                newResult.Add(word);
            }*/
            return result;
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            var mas = new char[word.Length];
            Array.Copy(word, mas, word.Length);
            
            if (startIndex == word.Length)
            {
                if(!result.Contains(new string(word)))
                    result.Add(new string(word));
                return;
            }

            if (!char.IsLetter(word[startIndex]))
                AlternateCharCases(word, startIndex + 1, result);
            
            var lower = char.ToLower(word[startIndex]);
            var upper = char.ToUpper(word[startIndex]);
            word[startIndex] = lower;
            AlternateCharCases(word, startIndex + 1, result);
            if (lower != upper)
            {
                word[startIndex] = upper;
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            List<string> allPasswords = CaseAlternatorTask.AlternateCharCases("ⅲ ⅳ ⅷ");
            foreach (var password in allPasswords)
            {
                Console.WriteLine(password);
            }
        }
    }
}