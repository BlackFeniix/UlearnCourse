using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procent
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString;
            inputString = Console.ReadLine();
            double credit = Procent.Calculate(inputString);
            Console.WriteLine(credit);
            System.Threading.Thread.Sleep(5000);

        }
    }
}
