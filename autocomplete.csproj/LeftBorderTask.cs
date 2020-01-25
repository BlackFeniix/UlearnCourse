﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Autocomplete
{
    // Внимание!
    // Есть одна распространенная ловушка при сравнении строк: строки можно сравнивать по-разному:
    // с учетом регистра, без учета, зависеть от кодировки и т.п.
    // В файле словаря все слова отсортированы методом StringComparison.OrdinalIgnoreCase.
    // Во всех функциях сравнения строк в C# можно передать способ сравнения.
    public class LeftBorderTask
    {
        /// <returns>
        /// Возвращает индекс левой границы.
        /// То есть индекс максимальной фразы, которая не начинается с prefix и меньшая prefix.
        /// Если такой нет, то возвращает -1
        /// </returns>
        /// <remarks>
        /// Функция должна быть рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            // IReadOnlyList похож на List, но у него нет методов модификации списка.
            // Этот код решает задачу, но слишком неэффективно. Замените его на бинарный поиск!
            if ((right - left) < 100)
            {
                for (var i = left+1; i < right-1; i++)
                {
                    if (string.Compare(prefix, phrases[i], StringComparison.OrdinalIgnoreCase) < 0
                        || phrases[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                        return i ;
                }
            }
            //return left;
            if ((right - left) >= 100)
            {
                var middle = left + (right - left)/2;
                if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) < 0) 
                    return GetLeftBorderIndex(phrases, prefix, left, middle);
                else return GetLeftBorderIndex(phrases, prefix, middle, right);
            }
            //if (phrases[right].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            //return right-1;
            
            /*for (int i = 0; i < phrases.Count; i++)
            {
                if (string.Compare(prefix, phrases[i], StringComparison.OrdinalIgnoreCase) < 0
                    || phrases[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return i - 1;
            }*/
            return phrases.Count-1;
        }
    }
}
