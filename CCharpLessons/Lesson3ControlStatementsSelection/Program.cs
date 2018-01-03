using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson3ControlStatementsSelection
{
    class IfSelect
    {
        static void SwithcSelect()
        {
            int myInt;
            string myInput;
        begin:
            Console.WriteLine("Please enter a number between 1 and 3:");
            myInput = Console.ReadLine();
            myInt = Int32.Parse(myInput);
            switch (myInt)
            {
                case 1:
                    Console.WriteLine("Your number is {0}", 1);
                    break;
                case 2:
                    Console.WriteLine("Your number is {0}", 2);
                    break;
                case 3:
                    Console.WriteLine("Your number is {0}", 3);
                    break;
                default:
                    goto begin;
            }

            switch (myInt)
            {
                case 1:
                case 2:
                case 3:
                    Console.WriteLine("Your number {0} is between 1 and 3", myInt);
                    break;
                default:
                    Console.WriteLine("Your number {0} not is between 1 and 3", myInt);
                    break;
            }

        }
        static void Main(string[] args)
        {
            string myInput;
            int myInt;

            Console.Write("Please enter a number");
            myInput = Console.ReadLine();
            myInt = Int32.Parse(myInput);
            if (myInt > 0)
                Console.WriteLine("Your number {0} is greater ten zero.", myInt);
            if (myInt < 0)
                Console.WriteLine("Your number {0} is less than zero.", myInt);

            if (myInt > 0)
                Console.WriteLine("Your number {0} is not equal to zero.", myInt);
            else
                Console.WriteLine("Your number {0} is equal to zero.", myInt);


            SwithcSelect();
            Console.ReadKey();

        }
    }
}