using System;
using System.Collections.Generic;
using System.Text;

namespace Lession21StaticClass
{
    static class MyMathClass
    {
        public static void WriteName()
        {
            Console.WriteLine("Static classes can have only static methods!!");
        }

        public static int Modulo(int a, int b)
        {
            return a % b;
        }

        public static double Power(double number, double exponent)
        {
            return Math.Pow(number, exponent);
        }
    }
}
