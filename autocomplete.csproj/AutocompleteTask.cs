using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            // тут стоит использовать написанный ранее класс LeftBorderTask
            var listOfIndex=new List<string>();
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count)+1;
            if (index==phrases.Count)
                return new string[0];
            var forBorder = Math.Min(count, phrases.Count - index);
            for (var i = 0; i < forBorder; i++)
            {
                /*if (i>-1 && i<phrases.Count)
                    listOfIndex.Add(phrases[i]);*/
                if (phrases[i+index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    listOfIndex.Add(phrases[i+index]);
            }
            return listOfIndex.ToArray();
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            if ((RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count)-LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count)-1)>0)
                return RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count)-LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count)-1;
            return 0;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            // ...
            //CollectionAssert.IsEmpty(actualTopWords);
        }

        // ...

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            // ...
            //Assert.AreEqual(expectedCount, actualCount);
        }

        // ...
    }
}
