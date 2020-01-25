using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var arrayOfDateForName = new string[31];
            var arrayOfAmountForName = new double[31];
            for (var i = 0; i < arrayOfDateForName.Length; i++)
                arrayOfDateForName[i] = (i+1).ToString();
            for (var i=0; i<names.Length; i++)
                if (names[i].Name.Equals(name))
                {
                    arrayOfAmountForName[names[i].BirthDate.Day-1]++;
                }
            arrayOfAmountForName[0] = 0;
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                arrayOfDateForName, 
                arrayOfAmountForName);
        }
    }
}