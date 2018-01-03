using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson4ControlStatementsLoops
{
    class Program
    {
        /// <summary>
        /// While loop
        /// </summary>
        static void WhileLoop()
        {
            int myInt = 0;
            while (myInt < 10)
            {
                Console.WriteLine("{0}", myInt);
                myInt++;
            }
        }

        /// <summary>
        /// Do while loop
        /// </summary>
        static void DoWhile()
        {
            string myChoice;

            do
            {
                // Print A Menu
                Console.WriteLine("My Address Book\n");

                Console.WriteLine("A - Add New Address");
                Console.WriteLine("D - Delete Address");
                Console.WriteLine("M - Modify Address");
                Console.WriteLine("V - View Addresses");
                Console.WriteLine("Q - Quit\n");

                Console.WriteLine("Choice (A,D,M,V,or Q): ");

                // Retrieve the user's choice
                myChoice = Console.ReadLine();

                // Make a decision based on the user's choice
                switch (myChoice)
                {
                    case "A":
                    case "a":
                        Console.WriteLine("You wish to add an address.");
                        break;
                    case "D":
                    case "d":
                        Console.WriteLine("You wish to delete an address.");
                        break;
                    case "M":
                    case "m":
                        Console.WriteLine("You wish to modify an address.");
                        break;
                    case "V":
                    case "v":
                        Console.WriteLine("You wish to view the address list.");
                        break;
                    case "Q":
                    case "q":
                        Console.WriteLine("Bye.");
                        break;
                    default:
                        Console.WriteLine("{0} is not a valid choice", myChoice);
                        break;
                }

                // Pause to allow the user to see the results
                Console.Write("press Enter key to continue...");
                Console.ReadLine();
                Console.WriteLine();
            } while (myChoice != "Q" && myChoice != "q"); // Keep going until the user wants to quit
        }

        /// <summary>
        /// For loop
        /// </summary>
        static void ForLoop()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("i={0}", i);
                if (i == 10)
                {
                    Console.WriteLine("i={0} break", i);
                    break;
                }

            }
        }

        /// <summary>
        /// For each loop
        /// </summary>
        static void ForEach()
        {
            string[] names = {"Chery", "Joe", "Matt", "Robert"};
            foreach (string s in names)
                Console.WriteLine(s);

            int[] numbers = { 55, 44, 33, 22, 11, 00 };
            foreach (int i in numbers)
                Console.WriteLine("i={0}", i);
        }


        static void Main(string[] args)
        {
            Console.WriteLine("--------while loop--------");
            WhileLoop();
            Console.WriteLine("--------Do while loop--------");
            DoWhile();
            Console.WriteLine("--------For loop--------");
            ForLoop();
            Console.WriteLine("--------For each--------");
            ForEach();
            Console.ReadKey();
            
        }
    }
}
