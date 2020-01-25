using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var arrayOfHeatMap = new double[30,12];
            for (var i = 0; i < names.Length; i++)
            {
                if (names[i].BirthDate.Day!=1)
                    arrayOfHeatMap[names[i].BirthDate.Day-2, names[i].BirthDate.Month-1]++;
            }

            var arrayOfDate = new string[30];
            var arrayOfMonth = new string[12];
            for (var i = 0; i < arrayOfDate.Length; i++)
                arrayOfDate[i] = (i + 2).ToString();
            for (var i = 0; i < arrayOfMonth.Length; i++)
                arrayOfMonth[i] = (i + 1).ToString();

            return new HeatmapData(
                "Пример карты интенсивностей",
                arrayOfHeatMap, 
                arrayOfDate,
                arrayOfMonth);
        }
    }
}