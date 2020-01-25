using System;

namespace CombineMassive
{
    class Program
    {
        static Array Combine(params Array[] array)
        {
            int boolean=0;
            int sizeOfArray=0;
            int positionInArray=-1;
            if (array.Length==0)
                return null;
            for (var i=0; i<array.Length-1; i++)
            {
                if (array[i].GetType().GetElementType() !=
                    array[i+1].GetType().GetElementType())
                    boolean=1;
                sizeOfArray+=array[i].Length;
            }
            sizeOfArray += array[array.Length - 1].Length;
            if (boolean==1)
                return null;
            var newArray=Array.CreateInstance(array[0].GetType().GetElementType(),sizeOfArray);
	
            for (var i=0; i<array.Length; i++)
            {
                for (var j=0; j<array[i].Length; j++)
                {
                    positionInArray=positionInArray+1;
                    newArray.SetValue(array[i].GetValue(j), positionInArray);
                }
            }
            return newArray;
        }
        
        static void Print(Array array)
        {
            if (array == null)
            {
                Console.WriteLine("null");
                return;
            }
            for (var i = 0; i < array.Length; i++)
                Console.Write("{0} ", array.GetValue(i));
            Console.WriteLine();
        }
        
        static void Main(string[] args)
        {
            var ints = new[] { 1, 2 };
            var strings = new[] { "A", "B" };

            Print(Combine(ints, ints));
            Print(Combine(ints, ints, ints));
            Print(Combine(ints));
            Print(Combine());
            Print(Combine(strings, strings));
            Print(Combine(ints, strings));        }
    }
}