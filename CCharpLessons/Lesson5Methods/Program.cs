using System;
using System.Collections.Generic;
using System.Text;

//==================================STATIC AND NON STATIC========================================
//The difference between instance methods and static methods is that multiple instances of a class 
//can be created (or instantiated) and each instance has its own separate getChoice() method. 
//However, when a method is static, there are no instances of that method, and you can invoke 
//only that one definition of the static method.

//====================================REFERENCE PARAMETER==========================================
//The addAddress() method takes a ref parameter. This means that a reference to the parameter is copied 
//to the method. This reference still refers to the same object on the heap as the original reference used 
//in the caller's argument. This means any changes to the local reference's object also changes the caller 
//reference's object. T

namespace Lesson5Methods
{
    /// <summary>
    /// A class to hold name and address
    /// </summary>
    class Person
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    class TheMethod
    {

        string getChoice()
        {
            string myChoice;
            // Print A Menu
            Console.WriteLine("My Address Book\n");
            Console.WriteLine("A - Add New Address");
            Console.WriteLine("D - Delete Address");
            Console.WriteLine("M - Modify Address");
            Console.WriteLine("V - View Addresses");
            Console.WriteLine("Q - Quit\n");
            Console.Write("Choice (A,D,M,V,or Q): ");

            // Retrieve the user's choice
            myChoice = Console.ReadLine();
            Console.WriteLine();
            return myChoice;
        }

        static void Main(string[] args)
        {
            string myChoice;
            TheMethod om = new TheMethod();
            do
            {
                myChoice = om.getChoice();
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
                Console.WriteLine();
                Console.Write("press Enter key to continue...");
                Console.ReadLine();
                Console.WriteLine();

            } while (myChoice != "Q" && myChoice != "q"); // Keep going until the user wants to quit
        }

       
    }
}
