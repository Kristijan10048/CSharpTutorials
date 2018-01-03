using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");

            int suma = 0;
            for (int i = 1; i <= 100; i++)
            {
                if (i % 2 == 0)
                    Console.WriteLine("Paren :{0}", i);

                suma = suma + i;
            }

            Console.WriteLine("Suma: {0}", suma);
            Console.ReadKey();
        }
    }
}
