using System;

namespace sem2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /* Первая задача*/
            /*
              int col3 = 999 / 3;
              int col5 = 999 / 5;
              int col15 = 999 / 15;
              int sum3 = (3 + 999) * col3 / 2;
              int sum5 = (3 + 999) * col5 / 2;
              int sum15 = (3 + 999) * col15 / 2;
              Console.WriteLine(sum3 + sum5 - sum15);
            */
            /* Вторая задача*/
            /*
            int h; // HOUR 
            int m; // MINUT
            h = int.Parse(Console.ReadLine());
            m = int.Parse(Console.ReadLine());
            double hAngle = 0.5D * (h * 60 + m);
            double mAngle = 6 * m;
            double angle = Math.Abs(hAngle - mAngle);
            angle = Math.Min(angle, 360 - angle);
            Console.WriteLine(angle);
            */
            /* Третья задача */
            /* double t_max;
            double t_min;
            int h, t, v ,x;
            h = int.Parse(Console.ReadLine());
            t = int.Parse(Console.ReadLine());
            v = int.Parse(Console.ReadLine());
            x = int.Parse(Console.ReadLine());
            t_min = (h - x * t) / (v - x);
            t_max = t;
            Console.WriteLine(t_min);
            Console.WriteLine(t_max);
            */
            /*Четвертая задача*/
            /* int a, r;
            double s = 0;
            a = int.Parse(Console.ReadLine());
            r = int.Parse(Console.ReadLine());
            if (2 * r <= a)
            {
                s = Math.PI * r * r;
            }
            if (2 * r > a && 2 * r < (a * Math.Sqrt(2)))
            {
                double z = Math.Sqrt(r * r - (a * a / 4));
                double angle = Math.Atan(z / (a * 0.5)) * 180.0 / Math.PI;
                double angleokr = 90.0 - 2 * angle;
                s = 4 * (z * (a / 2) + Math.PI * r * r * (angleokr / 360.0));
            }
            if (2 * r >= a * Math.Sqrt(2))
            {
                s = a * a;
            }
            Console.WriteLine(s);
            */

        }
    }
}
