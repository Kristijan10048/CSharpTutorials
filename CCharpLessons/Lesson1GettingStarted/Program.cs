using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1GettingStarted
{
    class WellcomeCSS
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wellcome to the c#");
            //The return value from this method replaces the "{0}" parameter of the formatted string 
            //and is written to the console. This line could have also been written like this:
            Console.WriteLine("Hello {0}!",Console.ReadLine());
            Console.ReadKey();
        }
    }
}
