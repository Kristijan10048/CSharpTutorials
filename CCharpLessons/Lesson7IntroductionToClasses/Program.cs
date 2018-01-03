using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson7IntroductionToClasses
{
    class OutputClass 
    {
        string myString;

        // Constructor
        public OutputClass(string inputString) 
        {
            Console.WriteLine("OutputClass:Constuctor");
            myString = inputString;
        }

        // Instance Method
        public void printString() 
        {
            Console.WriteLine("{0}", myString);
        }

        public static void nemozesDaMeViknes(string str)
        {
            Console.WriteLine("staticka metoda :{0}", str);
        }
        // Destructor
        ~OutputClass() 
        {
            Console.WriteLine("OutputClass:Destructor");
        // Some resource cleanup routines
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Instance of OutputClass
            OutputClass outCl = new OutputClass("This is printed by the output class.");

            // Call Output class' method
            outCl.printString();
            OutputClass.nemozesDaMeViknes("sho sakas?");
            Console.ReadKey();
        }
    }
}
