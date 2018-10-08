using System;

namespace Experiment
{
    class MainClass
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int num = 9; // количество цифр до n
            int i = 0; // количество цифр в данном числе
            int k = 9; // количество i значных чисел
            if (n >= 1 && n <= 9) Console.WriteLine(n);
            else
            { 
                //находим в скольки значных числах лежит искомое число
                while (num < n)
                {
                    num = num + i * k; 
                    if (i == 0) i++;
                    i++;
                    k = k * 10;
                }
                num = num - (i - 1) * (k / 10);
                i = Math.Abs(i - 1);
                num = n - num - 1;
                // q1 - число от первого i значного числа 6
                int q1 = num / i;
                // q - искомая цифра в этом числе
                int q = num % i;
                // находим число
                int chisl = (int)Math.Round(Math.Pow(10, i-1)) + q1;
                Console.WriteLine(chisl.ToString()[q]);
            }
        }
    }
}
