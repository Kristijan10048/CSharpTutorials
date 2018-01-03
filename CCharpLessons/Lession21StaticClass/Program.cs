using System;
using System.Collections.Generic;
using System.Text;

namespace Lession21StaticClass
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMathClass.WriteName();
            Console.WriteLine("Power:{0}", MyMathClass.Power(9, 2));
            Console.WriteLine("Modulo:{0}", MyMathClass.Modulo(4, 3));
            Console.ReadKey();
        }
    }
}
