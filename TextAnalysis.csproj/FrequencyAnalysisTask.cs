using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var dist = new Dictionary<string, Dictionary<string, int>>(); // Создание такого словаря
            //
            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    //bigramma search amount
                    if (!dist.ContainsKey(sentence[i]))
                    {
                        dist[sentence[i]] = new Dictionary<string, int>();
                        dist[sentence[i]][sentence[i + 1]] = 1;
                    }
                    else if (!dist[sentence[i]].ContainsKey(sentence[i + 1]))
                        dist[sentence[i]][sentence[i+1]]=1;
                    else
                        dist[sentence[i]][sentence[i + 1]]++;

                    //trigramma search amount
                    if (sentence.Count>2 && i!=sentence.Count-2)
                    {
                        string trigrammaKey = sentence[i] + " " + sentence[i + 1];

                        if (!dist.ContainsKey(trigrammaKey))
                        {
                            dist[trigrammaKey] = new Dictionary<string, int>();
                            dist[trigrammaKey][sentence[i + 2]] = 1;
                        }
                        else if (!dist[trigrammaKey].ContainsKey(sentence[i + 2]))
                            dist[trigrammaKey][sentence[i + 2]] = 1;
                        else
                            dist[trigrammaKey][sentence[i + 2]]++;

                    }
                }

            }
               
            foreach(var gramma in dist)
            {
                result.Add(gramma.Key, BigrammaAmountVer2(gramma.Value));
            }

            return result;
        }

    public static string BigrammaAmountVer2(Dictionary<string , int> dict)
        {
            var max = dict.Max(s => s.Value);
            var result = dict.Where(s => s.Value.Equals(max)).Select(s => s.Key).ToList();
            string leksLess = result[0];
            foreach (var str in result)
            {
                if (string.CompareOrdinal(str, leksLess) < 0)
                    leksLess = str;
            }
            return leksLess;
        }
    }
}