using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        //словарь с индексами слов (ключ - слово, значение - id документа)
        private Dictionary<string, List<int>> indexesDictionary;
        //словарь с ключом документом и значением текстом
        private Dictionary<int, string[]> documentDictionary;

        public Indexer()
        {
            documentDictionary = new Dictionary<int, string[]>();
            indexesDictionary = new Dictionary<string, List<int>>();
        }
//
        public void Add(int id, string documentText)
        {
            if (documentDictionary.ContainsKey(id))
                throw new ArgumentException();
            
            var wordsFromDocument= documentText.Split(' ', '.', ',', '!', '?', ':', '-','\r','\n');
            documentDictionary.Add(id,wordsFromDocument);
            
            foreach (var word in wordsFromDocument)
            {
                if (!indexesDictionary.ContainsKey(word))
                    indexesDictionary.Add(word, new List<int>() {id});
                else
                    indexesDictionary[word].Add(id);
            }
        }

        public List<int> GetIds(string word)
        {
            if (!indexesDictionary.ContainsKey(word))
                throw new ArgumentException();
            var listOfId= indexesDictionary[word];
            return listOfId;
        }

        public List<int> GetPositions(int id, string word)
        {           
            if (!documentDictionary.ContainsKey(id))
                throw new ArgumentException();
            
            var listOfPosition= new List<int>();
            var index = 0;
            foreach (var nextWord in documentDictionary[id])
            {
                if (word==nextWord)
                    listOfPosition.Add(index);
                index = index + nextWord.Length + 1;
            }
            return listOfPosition;
        }

        public void Remove(int id)
        {
            if (!documentDictionary.ContainsKey(id))
                throw new ArgumentException();
            documentDictionary.Remove(id);
            
            foreach (var pair in indexesDictionary)
            {
                if (pair.Value.Contains(id))
                {
                    pair.Value.Remove(id);
                }
            }
        }
    }
}
