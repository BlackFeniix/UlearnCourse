namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
            // Напишите функцию склонения слова "рублей" в зависимости от предшествующего числительного count.
            int lastNumber = count % 10;
            int twoLastNumbers = count % 100;
            if (count > 10 && twoLastNumbers > 10 && twoLastNumbers < 20)
                return "рублей";
            if (lastNumber == 1)
                return "рубль";
            else if (lastNumber == 2 || lastNumber == 3 || lastNumber == 4)
                return "рубля";
            else if (lastNumber==0 || lastNumber == 5 || lastNumber == 6 || lastNumber == 7 || lastNumber==8 || lastNumber==9)
                return "рублей";
            return "руб.";
		}
	}
}