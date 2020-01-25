using System;

namespace BinSearchRightBorder
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] array = new[] {2L, 2L, 2L, 3L, 4L};
            Console.WriteLine(FindLeftBorder( new []{ 1L, 2L, 3L}, 0));
            Console.WriteLine(FindLeftBorder(array, 3));
            
        }

        private static int FindLeftBorder(long[] arr, long value)
        {
            return BinSearchLeftBorder(arr, value, -1, arr.Length);
        }
        
        public static int BinSearchLeftBorder(long[] array, long value, int left, int right)
        {

            if ((right - left)==1) return left;
            var m = (left + right) / 2;
            if (array[m] < value)
                return BinSearchLeftBorder(array, value, m, right);
            return BinSearchLeftBorder(array, value, left, m);
        }
    }
}