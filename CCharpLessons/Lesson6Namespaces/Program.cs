using System;
using MyNameSpace;

namespace Lesson6Namespaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------Car Class------------------");
            MyNameSpace.Car myCar = new Car();
            myCar.Brand = "WV";
            myCar.Model = "Polo";
            myCar.Number = 1234;
            myCar.Print();


            Console.WriteLine("------------------Lessons Class------------------");
            MyNameSpace.Tutorial.Lessons l1 = new MyNameSpace.Tutorial.Lessons();
            l1.Title = "C# Best Practice";
            l1.Chapter = "Chapter 1";
            l1.Print();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();            
        }
    }
}
