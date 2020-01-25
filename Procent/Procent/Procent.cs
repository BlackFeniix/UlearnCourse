using System;

namespace Procent
{
    public static class Procent
    {
        public static double Calculate(string inputString)
        {
            String[] numsFromString = inputString.Split(' ');
            double initialAmount = Convert.ToDouble(numsFromString[0]);
            double interestRate = Convert.ToDouble(numsFromString[1]);
            int timeOfDeposit = Convert.ToInt32(numsFromString[2]);

            initialAmount = initialAmount * Math.Pow(1 + interestRate / 12 / 100, timeOfDeposit);
            return initialAmount;
        }
    }
}
