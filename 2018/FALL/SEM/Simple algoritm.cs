using System;

namespace Experiment
{
    class MainClass
    {
        //метод для 6 задачи
     /*   public static double SerchDistans(int x1, int y1, int x2, int y2)
        {
            double a = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            return a;
        }   */
        public static void Main(string[] args)
        {


            /* Вторая задача */
            /*  //Read stroka
              string str = Console.ReadLine();
              int number = int.Parse(str);
              //сотня
              int sto = number / 100;
              //еденицы
              int ten = number % 10;
              //десятки
              int center = (number / 10) % 10;  
              //преобразование чисел в строку
              string newStr1 = sto.ToString();
              string newStr2 = ten.ToString();
              string newStr3 = center.ToString();
              //вывод
              Console.WriteLine(newStr2 + newStr3 + newStr1);
              */





            /* Третья задача */
            /*
            string str = Console.ReadLine();
            int clock = int.Parse(str);
            int grad = 0;
            if (clock < 12 && clock > 6)
            {
                grad = 360 - clock * 30;
            }
            else
            {
                if (clock > 12)
                {
                    grad = ((24 - clock) * 30);
                }

                else {
                    grad = clock * 30;
                }
            }
            Console.WriteLine(grad);
            */




            /* Четвертая задача */
            /*      int n = int.Parse(Console.ReadLine());
                  int x = int.Parse(Console.ReadLine());
                  int y = int.Parse(Console.ReadLine());
                  Console.WriteLine(((n - 1) / x) + ((n - 1) / y) - ((n - 1) / (x + y)));
                  */



            /*Пятая задача*/
            /*  int a = int.Parse(Console.ReadLine());
                int b = int.Parse(Console.ReadLine());
                Console.WriteLine((b / 4 - b / 100 + b / 400) - (a / 4 - a / 100 + a / 400));

            */



            /*Шестая задача (нужно подключить метод) */
            /*  Console.WriteLine("Enter x1");
              int x1 = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter y1");
              int y1 = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter x2");
              int x2 = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter y2");
              int y2 = int.Parse(Console.ReadLine());
              Console.WriteLine("Ener point cordinat x");
              int x = int.Parse(Console.ReadLine());
              Console.WriteLine("Ener point cordinat y");
              int y = int.Parse(Console.ReadLine());
              double a = SerchDistans(x1, y1, x2, y2);
              double b = SerchDistans(x1, y1, x, y);
              double c = SerchDistans(x, y, x2, y2);
              double p = (a + b + c) / 2;
              double h = (2 / a) * Math.Sqrt(p * (p - a) * (p - b) * (p - c));
              Console.WriteLine("Ditans is " + h);
              */





            /*Седьмая задача*/
            /*  Console.WriteLine("Equation of the line is Ax + By +C = 0");
              Console.WriteLine("Enter A");
              int A = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter B");
              int B = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter C");
              int C = int.Parse(Console.ReadLine());
              Console.WriteLine("Perpendicular vektor is p({0},{1})", A,B);
              Console.WriteLine("Parallel vektor is p({0},{1})", -B, A);
              */



/*Восьмая задача*/

        /*  //задаваться будет равнение прямой вида Ax+By+c=0 и кордината точки
              Console.WriteLine("Enter A");
              int A = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter B");
              int B = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter C");
              int C = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter x");
              double x = int.Parse(Console.ReadLine());
              Console.WriteLine("Enter y");
              double y = int.Parse(Console.ReadLine());
              double C2 = -B * x + A * y;
              double x2 = 0;
              double y2 = 0;
              if (A == 0)  //если A = 0
              {
                  y2 = (-C) / B;
                  x2 = (-C2) / B;
              }
              if (B == 0) // если B = 0
              {
                  x2 = (-C) / A;
                  y2 = (C2) / A;
              }
              if (A != 0 && B != 0)
              {
                  y2 = (C2 * A - C * B) / (A * A + B * B);
                  x2 = (-A * C - C2 * B) / (B * B + A * A);
                  // Console.WriteLine("({0},{1})", x2, y2);
              }
              Console.WriteLine("({0};{1})", x2, y2);
              */
        }
        
    }
}
